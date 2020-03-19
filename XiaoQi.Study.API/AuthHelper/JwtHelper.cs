using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace XiaoQi.Study.API.AuthHelper
{
    public class JwtHelper
    {
        public static string IssueJwt(JwtTokenModel jwtTokenModel)
        {
            //声明Token得内容
            var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.Jti,jwtTokenModel.Uid),
               new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
               new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
               //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
               new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
               new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(1000).ToString()),
               new Claim(JwtRegisteredClaimNames.Iss,"Issuer"),
                new Claim(JwtRegisteredClaimNames.Aud,"Audience"),

           };
            claims.AddRange(jwtTokenModel.Roles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

            //设置 密钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xiaoqiyaozouxiaoqiyaozouxiaoqiyaozou"));
            //加密
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //创建 JwtSecurityToken 对象
            var jwt = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds
                );

            //实例化Token助手
            var jwtHandler = new JwtSecurityTokenHandler();

            //生成最终得Token
            var jwt_ = jwtHandler.WriteToken(jwt);
            return jwt_;
        }
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static JwtTokenModel JwtSerialize(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role;
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var jwtTokenModel = new JwtTokenModel
            {
                Uid = jwtToken.Id,
                Roles = role != null ? role.ToString() : "",
            };
            return jwtTokenModel;
        }
    }
}
