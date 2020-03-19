using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XiaoQi.Study.EF;

namespace XiaoQi.Study.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("MyPolicy")]
    public class ManagerController : ControllerBase
    {
        private readonly ILogger<ManagerController> _logger;
        private readonly IEFCoreService _EFCoreService;
        //private readonly IUserInfoService userService;
        public ManagerController(
            ILogger<ManagerController> logger,
            IEFCoreService efCoreService)
        {
            _logger = logger;
            _EFCoreService = efCoreService;
        }

        #region 用户
        /// <summary>
        /// get all userInfos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserInfos")]
        public IActionResult GetUserInfos()
        {
            //得到所有的用户人数
            var data = _EFCoreService.GetUserInfos();
            return new JsonResult(new Result { Data = data, Msg = "获取用户信息成功", Status = 200 });
        }
        /// <summary>
        /// add single userInfo
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUserInfo")]
        public IActionResult AddUserInfo(UserInfo userInfo)
        {
          
            var userId = Guid.NewGuid();
            userInfo.UserId = userId.ToString();
            userInfo.SetTime = System.DateTime.Now;

            var res = _EFCoreService.AddUserInfo(userInfo);

            return new JsonResult(new Result { Data = res, Msg = $"添加用户数据{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
        }
        /// <summary>
        /// 根据查询条件查询用户数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserInfoByQueryInfo")]
        public IActionResult GetUserInfoByQueryInfo(int pageSize, int pageIndex)
        {
            int total = 0;
            var pageData = _EFCoreService.GetUserPageEntities(pageSize, pageIndex, out total, true);
            var result = new { pageData = pageData, total = total };

            return new JsonResult(new Result { Data = result, Msg = "用户数据获取成功", Status = 200 });
        }
        /// <summary>
        /// 根据用户ID得到具体用户数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserByUserId")]
        public IActionResult GetUserInfoByUserId(string userId)
        {
            var userInfo = _EFCoreService.GetUserInfoByUserId(userId);

            return new JsonResult(new Result { Data = userInfo, Msg = "获取用户信息成功", Status = 200 });
        }
        /// <summary>
        /// 更新用户数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUserInfo")]
        public IActionResult UpdateUserInfo(UserInfo userInfo)
        {
            if (userInfo != null)
            {
                var res = _EFCoreService.UpdateUserInfos(userInfo);
                return new JsonResult(new Result { Data = res, Msg = $"更新用户数据{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
            }
            else
            {
                return new JsonResult(new Result { Data = null, Msg = "参数不对", Status = 204 });
            }
        }
        /// <summary>
        /// 删除用户数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteUserInfo")]
        public IActionResult DeleteUserInfoById(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var res = _EFCoreService.DeleteUserInfo(userId);

                return new JsonResult(new Result { Data = res, Msg = $"删除用户数据{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
            }
            else
            {
                return new JsonResult(new Result { Data = null, Msg = "参数不对", Status = 204 });
            }
        }
        #endregion

        #region 角色
        /// <summary>
        /// 根据分页条件得到角色信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRoleInfos")]
        public IActionResult GetRoleInfosByQueryInfo(int pageSize, int pageIndex)
        {
            int total = 0;
            var roleData = _EFCoreService.GetRolPageEnties(pageSize, pageIndex, out total, true);
            var res = new { roleData, total };

            return new JsonResult(new Result { Data = res, Msg = "获取角色信息成功", Status = 200 });

        }
        /// <summary>
        /// 无条件获取所有的角色信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllRoles")]
        public IActionResult GetAllRoleInfos()
        {
            var res = _EFCoreService.GetRoleInfo();

            return new JsonResult(new Result { Data = res, Msg = "获取角色信息成功", Status = 200 });
        }
        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddRoleInfo")]
        public IActionResult AddRoleInfo(RoleInfo roleInfo)
        {
            string contenttype = Request.Headers["Content-Type"];


            roleInfo.RoleId = Guid.NewGuid().ToString();
            roleInfo.SetTime = DateTime.Now;
            var res = _EFCoreService.AddRoleInfo(roleInfo);

            return new JsonResult(new Result { Data = res, Msg = $"添加角色{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
        }
        /// <summary>
        /// 更改角色信息
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateRoleInfo")]
        public IActionResult UpdateRoleInfo(RoleInfo roleInfo)
        {
            roleInfo.SetTime = DateTime.Now;
            var res = _EFCoreService.UpdateRoleInfo(roleInfo);

            return new JsonResult(new Result { Data = res, Msg = $"更改角色{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
        }
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteRoleInfo")]
        public IActionResult DeleteRoleInfo(string roleId)
        {
            var res = _EFCoreService.DeleteRoleInfo(roleId);

            return new JsonResult(new Result { Data = res, Msg = $"删除角色{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
        }
        /// <summary>
        /// 根据角色ID 得到角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRoleInfoById")]
        public IActionResult GetRoleInfoById(string roleId)
        {
            var res = _EFCoreService.GetRoleInfoByID(roleId);

            return new JsonResult(new Result { Data = res, Msg = "成功获取角色信息", Status = 200 });
        }
        #endregion

        #region 菜单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuInfoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMenuById")]
        public IActionResult GetMenuById(string menuInfoId)
        {
            var res = _EFCoreService.GetMenuInfoById(menuInfoId);

            return new JsonResult(new Result { Data = res, Msg = "成功获取菜单信息", Status = 200 });
        }
        /// <summary>
        /// 得到所有的菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllMenu")]
        public IActionResult GetAllMenuInfo()
        {
            var menuInfos = _EFCoreService.GetAllMenuInfo();
            var count = menuInfos.Count();
            var res = new
            {
                menuData = menuInfos,
                total = count
            };

            return new JsonResult(new Result { Data = res, Msg = "成功获取菜单信息", Status = 200 });
        }
        /// <summary>
        /// 得到整个菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMenuTree")]
        public IActionResult GetMenuTree()
        {
            var menuInfos = _EFCoreService.GetAllMenuInfo();
            var res = _EFCoreService.GetMenuTree(menuInfos, "0");

            return new JsonResult(new Result { Data = res, Msg = "成功获取菜单信息", Status = 200 });
        }
        /// <summary>
        /// 添加菜单信息
        /// </summary>
        /// <param name="menuInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMenuInfo")]
        public IActionResult AddMenuInfo(MenuInfo menuInfo)
        {
            menuInfo.MenuInfoId = Guid.NewGuid().ToString();
            menuInfo.SetTime = DateTime.Now;
            var res = _EFCoreService.AddMenuInfo(menuInfo);

            return new JsonResult(new Result { Data = res, Msg = "成功添加菜单信息", Status = 200 });
        }
        /// <summary>
        /// 根据Id删除菜单信息
        /// </summary>
        /// <param name="menuInfoId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteMenuInfo")]
        public IActionResult DeleteMenuInfo(string menuInfoId)
        {
            var res = _EFCoreService.DeleteMenuInfo(menuInfoId);

            return new JsonResult(new Result { Data = res, Msg = $"菜单信息删除{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
        }
        /// <summary>
        /// 更改菜单信息
        /// </summary>
        /// <param name="menuInfo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateMenuInfo")]
        public IActionResult UpdateMenuInfo(MenuInfo menuInfo)
        {
            var res = _EFCoreService.UpdateMenuInfo(menuInfo);

            return new JsonResult(new Result { Data = res, Msg = $"菜单信息更新{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
        }
        #endregion

        #region 用户角色
        [HttpPost]
        [Route("SetUserRole")]
        public IActionResult SetUserRole(UserRole_R userRole)
        {
            var tmpData = _EFCoreService.GetUserRoleByUserId(userRole.UserId);
            if (tmpData != null)
            {
                tmpData.SetTime = DateTime.Now;
                tmpData.RoleId = userRole.RoleId;
                var res = _EFCoreService.UpdateUserRole(tmpData);

                return new JsonResult(new Result { Data = res, Msg = $"设置{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
            }
            else
            {
                userRole.UserRoleId = Guid.NewGuid().ToString();
                userRole.SetTime = DateTime.Now;
                var res = _EFCoreService.SetUserRole(userRole);

                return new JsonResult(new Result { Data = res, Msg = $"设置{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
            }

        }
        [HttpPut]
        [Route("UpdateUserRole")]
        public IActionResult UpdateUserRole(string userRoleId)
        {
            var userRole = _EFCoreService.GetUserRoleById(userRoleId);
            var res = _EFCoreService.UpdateUserRole(userRole);

            return new JsonResult(new Result { Data = res, Msg = $"更新{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
        }
        [HttpDelete]
        [Route("DeleteUserRole")]
        public IActionResult DeleteUserRole(string userRoleId)
        {
            var res = _EFCoreService.DeleteUserRoleById(userRoleId);
            return new JsonResult(new Result { Data = res, Msg = $"删除{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
        }
        #endregion

        #region 角色菜单 
        [HttpPost]
        [Route("SetRoleMenu")]
        public IActionResult SetRoleMenu(RoleMenu_R roleMenu)
        {
            var tmpData = _EFCoreService.GetRoleMenuByRoleId(roleMenu.RoleId);
            if (tmpData != null)
            {
                tmpData.SetTime = DateTime.Now;
                tmpData.MenuInfoId = roleMenu.MenuInfoId;
                var res = _EFCoreService.UpdateRoleMenu(tmpData);

                return new JsonResult(new Result { Data = res, Msg = $"设置{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
            }
            else
            {
                roleMenu.RoleMenuId = Guid.NewGuid().ToString();
                roleMenu.SetTime = DateTime.Now;
                var res = _EFCoreService.SetRoleMenu(roleMenu);

                return new JsonResult(new Result { Data = res, Msg = $"设置{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
            }
        }
        //[HttpGet]
        //[Route("GetRoleMenuIdsByRoleId")]
        //public IActionResult GetRoleMenuIdsByRoleId(string roleId)
        //{
        //    var data = _EFCoreService.GetRoleMenuByRoleId(roleId);
        //    return new JsonResult(data);
        //}
        [HttpGet]
        [Route("GetRoleMenusByRoleId")]
        public IActionResult GetRoleMenusByRoleId(string roleId)
        {
            var roleMenu = _EFCoreService.GetRoleMenuByRoleId(roleId);
            if (roleMenu != null)
            {
                string menuIds = roleMenu.MenuInfoId;
                var res = _EFCoreService.GetRoleMenus(menuIds);

                return new JsonResult(new Result { Data = res, Msg = "获取成功", Status = 200 });
            }
            return new JsonResult(new Result { Data = null, Msg = "获取失败", Status = 200 });

        }
        #endregion
    }
}