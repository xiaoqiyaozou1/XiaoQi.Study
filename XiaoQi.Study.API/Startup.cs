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
using XiaoQi.Study.IService;
using XiaoQi.Study.Service;
using XiaoQi.Study.Repository;
using XiaoQi.Study.IRepository;
using Autofac;

namespace XiaoQi.Study.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            LogHelper.Configure(); //使用前先配置
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注册控制器 并追加控制的全局过滤
            services.AddControllers(o =>
            {
                o.Filters.Add(typeof(GlobalExceptionFilter));
            });

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            //注册EF 上下文
            // services.AddDbContext<MyContext>(
            //     o => o.UseSqlite(@"Data Source=D:\Code\Project\XiaoQi.Study\XiaoQi.Study.API\DB\userinfo.db")
            //);

            services.AddDbContext<MyContext>(
            o => o.UseSqlite(@"Data Source="+ basePath + "userinfo.db")
       );
            //Swagger 相关注册
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "XiaoQi API", Version = "v1" });

                //为Swagger json 和 UI 增加注释信息
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //注册封装好的EFCoreService
            services.AddScoped<IEFCoreService, EFCoreService>();

            #region 注册所有得数据服务
            //services.AddTransient<IMenuButtonRepository, MenuButtonRepository>();
            //services.AddTransient<IMenuInfoRepository, MenuInfoRepository>();
            //services.AddTransient<IRoleInfoRepository, RoleInfoRepository>();
            //services.AddTransient<IRoleMenuRepository, RoleMenuRepository>();
            //services.AddTransient<IUserInfoRepository, UserInfoRepository>();
            //services.AddTransient<IUserRoleRepository, UserRoleRepository>();

            //services.AddTransient<IMenuButtonService, MenuButtonService>();
            //services.AddTransient<IMenuInfoService, MenuInfoService>();
            //services.AddTransient<IRoleInfoService, RoleInfoService>();
            //services.AddTransient<IRoleMenuService, RoleMenuService>();
            //services.AddTransient<IUserInfoService, UserInfoService>();
            //services.AddTransient<IUserRoleService, UserRoleService>();
            #endregion

            //注册此接口，给JwtAuthorizationHandler.cs 用
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            #region 权限验证相关
            // 授权验证的资源
            var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xiaoqiyaozouxiaoqiyaozouxiaoqiyaozou"));//加密验证的key         
            var jwtCreds = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256); //根据key' 生成的标识
            var jwtUserRoleInofs = new List<JwtUserRoleInfo>();//用户角色和用户拥有的api 集合 ，该角色只能访问其拥有的api   

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

            services.AddSingleton(jwtRequirement);//将该资源注册，可以在验证处理器种设置其值
            //注册策略授权
            services.AddAuthorization(o =>
            {
                o.AddPolicy("MyPolicy", policy => policy.Requirements.Add(jwtRequirement));
            });

            //验证参数设置
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jwtKey,
                ValidateIssuer = true,
                ValidIssuer = "Issuer",//发行人
                ValidateAudience = true,
                ValidAudience = "Audience",//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,
            };

            // 开启Bearer认证
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = nameof(ApiResponseHandler);
                o.DefaultForbidScheme = nameof(ApiResponseHandler);
            })
             // 添加JwtBearer服务
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = tokenValidationParameters;
                 o.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         // 如果过期，则把<是否过期>添加到，返回头信息中
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                 };
             })
             .AddScheme<AuthenticationSchemeOptions, ApiResponseHandler>(nameof(ApiResponseHandler), o => { });

            // 注入权限处理器
            services.AddScoped<IAuthorizationHandler, JwtAuthorizationHandler>();

            #endregion

            //注册跨域
            services.AddCors(options =>
            {
                string[] arr = { "http://192.168.1.3:9999", "http://localhost:8080", "http://152.136.33.250:6004" };
                options.AddPolicy("XiaoQiAllowOrigins",
                builder =>
                {
                    builder.WithOrigins(arr)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });

        }
        //Autofac容器
        public void ConfigureContainer(ContainerBuilder builder)
        {

            builder.RegisterType<MenuButtonRepository>().As<IMenuButtonRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MenuInfoRepository>().As<IMenuInfoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleInfoRepository>().As<IRoleInfoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleMenuRepository>().As<IRoleMenuRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserInfoRepository>().As<IUserInfoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRoleRepository>().As<IUserRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MenuButtonService>().As<IMenuButtonService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuInfoService>().As<IMenuInfoService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleInfoService>().As<IRoleInfoService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleMenuService>().As<IRoleMenuService>().InstancePerLifetimeScope();
            builder.RegisterType<UserInfoService>().As<IUserInfoService>().InstancePerLifetimeScope();
            builder.RegisterType<UserRoleService>().As<IUserRoleService>().InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //启动Swagger中间件相关 服务

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            //启用注册好的跨域
            app.UseCors("XiaoQiAllowOrigins");

            //认证
            app.UseAuthentication();

            //授权
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
