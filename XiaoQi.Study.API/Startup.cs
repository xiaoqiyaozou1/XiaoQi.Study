using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using XiaoQi.Study.EF;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using XiaoQi.Study.API.Common;
using Microsoft.IdentityModel.Tokens;
using XiaoQi.Study.API.AuthHelper;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using XiaoQi.Study.API.Filter;

namespace XiaoQi.Study.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            LogHelper.Configure(); //ʹ��ǰ������
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //ע������� ��׷�ӿ��Ƶ�ȫ�ֹ���
            services.AddControllers(o=>
            {
                o.Filters.Add(typeof(GlobalExceptionFilter));
            });


            //ע��EF ������
            services.AddDbContext<MyContext>(
                o => o.UseSqlite(@"Data Source=D:\Code\Project\XiaoQi.Study\XiaoQi.Study.API\DB\userinfo.db")
           );

            //Swagger ���ע��
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "XiaoQi API", Version = "v1" });

                //ΪSwagger json �� UI ����ע����Ϣ
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //ע���װ�õ�EFCoreService
            services.AddScoped<IEFCoreService, EFCoreService>();

            //ע��˽ӿڣ���JwtAuthorizationHandler.cs ��
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            #region Ȩ����֤���
            // ��Ȩ��֤����Դ
            var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xiaoqiyaozouxiaoqiyaozouxiaoqiyaozou"));//������֤��key         
            var jwtCreds = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256); //����key' ���ɵı�ʶ
            var jwtUserRoleInofs = new List<JwtUserRoleInfo>();//�û���ɫ���û�ӵ�е�api ���� ���ý�ɫֻ�ܷ�����ӵ�е�api   
            
            var jwtRequirement = new JwtAuthorizationRequirement(
                jwtUserRoleInofs,
                "",
                ClaimTypes.Role,
                expiration: TimeSpan.FromSeconds(60 * 60),
                "",
                "Issuer",
                "Audience",
                jwtCreds
                );

            services.AddSingleton(jwtRequirement);//������Դע�ᣬ��������֤��������������ֵ
            //ע�������Ȩ
            services.AddAuthorization(o =>
            {
                o.AddPolicy("MyPolicy", policy => policy.Requirements.Add(jwtRequirement));
            });

            //��֤��������
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jwtKey,
                ValidateIssuer = true,
                ValidIssuer = "Issuer",//������
                ValidateAudience = true,
                ValidAudience = "Audience",//������
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,
            };

            // ����Bearer��֤
            services.AddAuthentication(o => {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = nameof(ApiResponseHandler);
                o.DefaultForbidScheme = nameof(ApiResponseHandler);
            })
             // ����JwtBearer����
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = tokenValidationParameters;
                 o.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         // ������ڣ����<�Ƿ����>���ӵ�������ͷ��Ϣ��
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                 };
             })
             .AddScheme<AuthenticationSchemeOptions, ApiResponseHandler>(nameof(ApiResponseHandler), o => { });

            // ע��Ȩ�޴�����
            services.AddScoped<IAuthorizationHandler, JwtAuthorizationHandler>();

            #endregion

            //ע�����
            services.AddCors(options =>
            {
                string[] arr = { "http://192.168.1.3:9999", "http://localhost:8080" };
                options.AddPolicy("XiaoQiAllowOrigins",
                builder =>
                {
                    builder.WithOrigins(arr)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //����Swagger�м����� ����

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            //����ע��õĿ���
            app.UseCors("XiaoQiAllowOrigins");

            //��֤
            app.UseAuthentication();

            //��Ȩ
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}