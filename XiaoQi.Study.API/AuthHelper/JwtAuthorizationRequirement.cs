using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XiaoQi.Study.API.AuthHelper
{
    /// <summary>
    /// 授权的条件
    /// </summary>
    public class JwtAuthorizationRequirement : IAuthorizationRequirement
    {


        public JwtAuthorizationRequirement(List<JwtUserRoleInfo> jwtUserRoleInofs, string deniedAction, string claimType, TimeSpan expiration, string loginPath, string issuer, string audience, SigningCredentials signingCredentials)
        {
            this.jwtUserRoleInofs = jwtUserRoleInofs;
            DeniedAction = deniedAction;
            ClaimType = claimType;
            Expiration = expiration;
            LoginPath = loginPath;
            Issuer = issuer;
            Audience = audience;
            SigningCredentials = signingCredentials;
        }

        /// <summary>
        /// 这个集合表示拥有访问权限的用户角色
        /// </summary>
        public List<JwtUserRoleInfo> jwtUserRoleInofs { get; set; }

        /// <summary>
        /// 无权限action？
        /// </summary>
        public string DeniedAction { get; set; }

        /// <summary>
        /// 认证授权类型
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// 过期时间长度
        /// </summary>
        public TimeSpan Expiration { get; set; }

        /// <summary>
        /// 请求路径
        /// </summary>
        public string LoginPath { get; set; } = "/Api/Login";
        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 订阅人
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 签名信息
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}
