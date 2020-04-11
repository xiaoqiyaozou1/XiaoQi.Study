using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XiaoQi.Study.API.AuthHelper;
using XiaoQi.Study.API.Common;
using XiaoQi.Study.IService;

namespace XiaoQi.Study.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IEFCoreService _EFCoreService;
        private readonly JwtAuthorizationRequirement _jwtRequirement;

        private readonly IUserInfoService _userInfoService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleInfoService _roleInfoService;
        public LoginController(
            IEFCoreService efCoreService,
            JwtAuthorizationRequirement jwtRequirement,
            IUserInfoService userInfoService,
            IUserRoleService userRoleService,
            IRoleInfoService roleInfoService)
        {
            _EFCoreService = efCoreService;
            _jwtRequirement = jwtRequirement;
            _userInfoService = userInfoService;
            _userRoleService = userRoleService;
            _roleInfoService = roleInfoService;
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult GetJwtToken3(DTO_UserInfo userInfo)
        {

            var data = _EFCoreService.GetByCountPwd(userInfo.UserName, userInfo.PassWord);
            //var userInfo = _EFCoreService.
            if (data != null)
            {
                #region 写死测试
                //这一部分写死了，应该从数据库取
                //var list = new List<JwtUserRoleInfo>();
                //var jwtUserRole = new JwtUserRoleInfo();
                //jwtUserRole.Role = "admin";
                //jwtUserRole.Url = "/Manager/GetUserInfos";
                //list.Add(jwtUserRole);
                //list.Add(new JwtUserRoleInfo
                //{
                //    Role = "admin",
                //    Url = "/Manager/GetUserInfoByQueryInfo"
                //});
                //list.Add(
                //    new JwtUserRoleInfo {
                //        Role = "admin",
                //        Url = "/Manager/AddUserInfo"
                //    }


                //);
                #endregion

                //从数据库获取 角色和api
                _jwtRequirement.jwtUserRoleInofs = _EFCoreService.GetAllRoleMenu();

                var roles = _EFCoreService.GetRoleInfos(data.UserId);
                var jwtTokenModle = new JwtTokenModel();
                jwtTokenModle.Uid = data.UserId;
                jwtTokenModle.Roles = roles;
                var token = JwtHelper.IssueJwt(jwtTokenModle);

                //记录登录日志 
                LogHelper.Info($"{("用户" + data.Name + "于" + System.DateTime.Now + "登录成功")}");

                return new JsonResult(new Result { Data = token, Msg = "获取Token成功", Status = 200 });
            }
            else
            {
                //记录登录日志 
                LogHelper.Info($"{("账号" + userInfo.UserName + "于" + System.DateTime.Now + "登录失败")}");
                return new JsonResult(new Result { Data = null, Msg = "获取Token失败", Status = 204 });
            }
        }

        [HttpPost]
        public async  Task<IActionResult> GetJwt(DTO_UserInfo userInfo)
        {
            var data = await _userInfoService.QueryByWhereAsync(o => o.Name == userInfo.UserName && o.Pwd == userInfo.PassWord);
            if (data != null)
            {
                //获取角色ID
                var userRole = await _userRoleService.QueryByWhereAsync(o => o.UserId == data.UserId);

                var roleIds = userRole.RoleId.Split(',').ToList();
                StringBuilder sb = new StringBuilder();
                foreach (var item in roleIds)
                {
                    var role = await _roleInfoService.QueryById(item);
                    sb.Append(role.Role);
                    sb.Append(",");
                }
                var roles = sb.ToString().TrimEnd(',');
                //从数据库获取 角色和api
                _jwtRequirement.jwtUserRoleInofs = _EFCoreService.GetAllRoleMenu();

    
                var jwtTokenModle = new JwtTokenModel();
                jwtTokenModle.Uid = data.UserId;
                jwtTokenModle.Roles = roles;
                var token = JwtHelper.IssueJwt(jwtTokenModle);

                //记录登录日志 
                LogHelper.Info($"{("用户" + data.Name + "于" + System.DateTime.Now + "登录成功")}");

                return new JsonResult(new Result { Data = token, Msg = "获取Token成功", Status = 200 });
            }
            else
            {
                //记录登录日志 
                LogHelper.Info($"{("账号" + userInfo.UserName + "于" + System.DateTime.Now + "登录失败")}");
                return new JsonResult(new Result { Data = null, Msg = "获取Token失败", Status = 204 });
            }
        }
    }
}