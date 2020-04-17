using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XiaoQi.Study.EF;
using XiaoQi.Study.IService;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("MyPolicy")]
    public class ManagerController : ControllerBase
    {
        private readonly ILogger<ManagerController> _logger;
        private readonly IEFCoreService _EFCoreService;

        private readonly IMenuButtonService _menuButtonService;
        private readonly IMenuInfoService _menuInfoService;
        private readonly IRoleInfoService _roleInfoService;
        private readonly IRoleMenuService _roleMenuService;
        private readonly IUserInfoService _userInfoService;
        private readonly IUserRoleService _userRoleService;


        //private readonly IUserInfoService userService;
        public ManagerController(
            ILogger<ManagerController> logger,
            IEFCoreService efCoreService,
            IMenuButtonService menuButtonService,
            IMenuInfoService menuInfoService,
            IRoleInfoService roleInfoService,
            IRoleMenuService roleMenuService,
            IUserInfoService userInfoService,
            IUserRoleService userRoleService
            )
        {
            _logger = logger;
            _EFCoreService = efCoreService;
            _menuButtonService = menuButtonService;
            _menuInfoService = menuInfoService;
            _roleInfoService = roleInfoService;
            _roleMenuService = roleMenuService;
            _userInfoService = userInfoService;
            _userRoleService = userRoleService;
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
        public async Task<IActionResult> AddUserInfo(UserInfo userInfo)
        {

            var userId = Guid.NewGuid();
            userInfo.UserId = userId.ToString();
            userInfo.SetTime = System.DateTime.Now;

            //var res = _EFCoreService.AddUserInfo(userInfo);
            var res = await _userInfoService.Add(userInfo);

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

            var pageInfo = _userInfoService.GetPageInfos(pageIndex, pageSize, out total, o => o.SetTime == o.SetTime, o => o.SetTime, true);

            //var pageData = _EFCoreService.GetUserPageEntities(pageSize, pageIndex, out total, true);
            var result = new { pageData = pageInfo, total = total };

            return new JsonResult(new Result { Data = result, Msg = "用户数据获取成功", Status = 200 });
        }
        /// <summary>
        /// 根据用户ID得到具体用户数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserByUserId")]
        public async Task<IActionResult> GetUserInfoByUserId(string userId)
        {
            //var userInfo = _EFCoreService.GetUserInfoByUserId(userId);
            var userInfo = await _userInfoService.QueryById(userId);
            return new JsonResult(new Result { Data = userInfo, Msg = "获取用户信息成功", Status = 200 });
        }
        /// <summary>
        /// 更新用户数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo(UserInfo userInfo)
        {
            if (userInfo != null)
            {
                // var res = _EFCoreService.UpdateUserInfos(userInfo);
                var res = await _userInfoService.Update(userInfo);
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
        public async Task<IActionResult> DeleteUserInfoById(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                //var res = _EFCoreService.DeleteUserInfo(userId);
                var userInfo = await _userInfoService.QueryById(userId);
                var res = await _userInfoService.Delete(userInfo);

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
            //var roleData = _EFCoreService.GetRolPageEnties(pageSize, pageIndex, out total, true);

            var roleInfo = _roleInfoService.GetPageInfos(pageIndex, pageSize, out total, o => o.RoleId == o.RoleId, o => o.SetTime, true);
            var res = new { roleData = roleInfo, total };

            return new JsonResult(new Result { Data = res, Msg = "获取角色信息成功", Status = 200 });

        }
        /// <summary>
        /// 无条件获取所有的角色信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoleInfos()
        {
            //var res = _EFCoreService.GetRoleInfo();

            var res = await _roleInfoService.QueryAsync();

            return new JsonResult(new Result { Data = res, Msg = "获取角色信息成功", Status = 200 });
        }
        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddRoleInfo")]
        public async Task<IActionResult> AddRoleInfo(RoleInfo roleInfo)
        {


            roleInfo.RoleId = Guid.NewGuid().ToString();
            roleInfo.SetTime = DateTime.Now;
            //  var res = _EFCoreService.AddRoleInfo(roleInfo);
            var res = await _roleInfoService.Add(roleInfo);

            return new JsonResult(new Result { Data = res, Msg = $"添加角色{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
        }
        /// <summary>
        /// 更改角色信息
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateRoleInfo")]
        public async Task<IActionResult> UpdateRoleInfo(RoleInfo roleInfo)
        {
            roleInfo.SetTime = DateTime.Now;
            //var res = _EFCoreService.UpdateRoleInfo(roleInfo);
            var res = await _roleInfoService.Update(roleInfo);
            return new JsonResult(new Result { Data = res, Msg = $"更改角色{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
        }
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteRoleInfo")]
        public async Task<IActionResult> DeleteRoleInfo(string roleId)
        {
            // var res = _EFCoreService.DeleteRoleInfo(roleId);
            var roleInfo = await _roleInfoService.QueryById(roleId);
            var res = await _roleInfoService.Delete(roleInfo);
            return new JsonResult(new Result { Data = res, Msg = $"删除角色{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
        }
        /// <summary>
        /// 根据角色ID 得到角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRoleInfoById")]
        public async Task<IActionResult> GetRoleInfoById(string roleId)
        {
            //var res = _EFCoreService.GetRoleInfoByID(roleId);
            var res = await _roleInfoService.QueryById(roleId);
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
        public async Task<IActionResult> GetAllMenuInfo()
        {
            var menuInfos = await _menuInfoService.QueryAsync();
            //  var menuInfos = _EFCoreService.GetAllMenuInfo();
            var count = menuInfos.Count();
            var res = new
            {
                menuData = menuInfos,
                total = menuInfos.Count
            };

            return new JsonResult(new Result { Data = res, Msg = "成功获取菜单信息", Status = 200 });
        }

        [HttpGet]
        [Route("GetMenusByQueryInfo")]
        public IActionResult GetMenuInfosByQueryInfo(int pageSize, int pageIndex)
        {
            int total = 0;
  

            var menuData =  _menuInfoService.GetPageInfos(pageIndex, pageSize, out total, o => o.FatherId == o.FatherId, o => o.FatherId, true);
            var res = new { menuData, total };

            return new JsonResult(new Result { Data = res, Msg = "获取菜单信息成功", Status = 200 });
        }

        /// <summary>
        /// 得到整个菜单树 2020 0412
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMenuTree")]
        public async Task<IActionResult> GetMenuTree()
        {
            var menuInfos = _EFCoreService.GetAllMenuInfo();
            //var res = _EFCoreService.GetMenuTree(menuInfos, "0");

            var res = await _menuInfoService.GetMenuTree();

            return new JsonResult(new Result { Data = res, Msg = "成功获取菜单信息", Status = 200 });
        }
        /// <summary>
        /// 添加菜单信息
        /// </summary>
        /// <param name="menuInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMenuInfo")]
        public async Task<IActionResult> AddMenuInfo(MenuInfo menuInfo)
        {
            menuInfo.MenuInfoId = Guid.NewGuid().ToString();
            menuInfo.SetTime = DateTime.Now;


            // var res = _EFCoreService.AddMenuInfo(menuInfo);
            var res = await _menuInfoService.Add(menuInfo);

            return new JsonResult(new Result { Data = res, Msg = "成功添加菜单信息", Status = 200 });
        }
        /// <summary>
        /// 根据Id删除菜单信息
        /// </summary>
        /// <param name="menuInfoId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteMenuInfo")]
        public async Task<IActionResult> DeleteMenuInfo(string menuInfoId)
        {
            //v ar res = _EFCoreService.DeleteMenuInfo(menuInfoId);
            var menuInfo = await _menuInfoService.QueryById(menuInfoId);
            var res = await _menuInfoService.Delete(menuInfo);
            return new JsonResult(new Result { Data = res, Msg = $"菜单信息删除{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
        }
        /// <summary>
        /// 更改菜单信息
        /// </summary>
        /// <param name="menuInfo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateMenuInfo")]
        public async Task<IActionResult> UpdateMenuInfo(MenuInfo menuInfo)
        {
            // var res = _EFCoreService.UpdateMenuInfo(menuInfo);
            var res = await _menuInfoService.Update(menuInfo);
            return new JsonResult(new Result { Data = res, Msg = $"菜单信息更新{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
        }
        #endregion

        #region 用户角色
        [HttpPost]
        [Route("SetUserRole")]
        public async Task<IActionResult> SetUserRole(UserRole_R userRole)
        {
            // var tmpData = _EFCoreService.GetUserRoleByUserId(userRole.UserId);
            var tmpData = await _userRoleService.QueryById(userRole.UserId);
            if (tmpData != null)
            {
                tmpData.SetTime = DateTime.Now;
                tmpData.RoleId = userRole.RoleId;
                // var res = _EFCoreService.UpdateUserRole(tmpData);
                var res = await _userRoleService.Update(tmpData);
                return new JsonResult(new Result { Data = res, Msg = $"设置{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
            }
            else
            {
                userRole.UserRoleId = Guid.NewGuid().ToString();
                userRole.SetTime = DateTime.Now;
                // var res = _EFCoreService.SetUserRole(userRole);
                var res = await _userRoleService.Add(userRole);
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
        public async Task<IActionResult> DeleteUserRole(string userRoleId)
        {

            //var res = _EFCoreService.DeleteUserRoleById(userRoleId);
            var userRole = await _userRoleService.QueryById(userRoleId);
            var res = await _userRoleService.Delete(userRole);

            return new JsonResult(new Result { Data = res, Msg = $"删除{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
        }
        #endregion

        #region 角色菜单 
        [HttpPost]
        [Route("SetRoleMenu")]
        public async Task<IActionResult> SetRoleMenu(RoleMenu_R roleMenu)
        {
            // var tmpData = _EFCoreService.GetRoleMenuByRoleId(roleMenu.RoleId);
            var tmpData = await _roleMenuService.QueryById(roleMenu.RoleId);
            if (tmpData != null)
            {
                tmpData.SetTime = DateTime.Now;
                tmpData.MenuInfoId = roleMenu.MenuInfoId;
                var res = await _roleMenuService.Update(tmpData);

                return new JsonResult(new Result { Data = res, Msg = $"设置{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
            }
            else
            {
                roleMenu.RoleMenuId = Guid.NewGuid().ToString();
                roleMenu.SetTime = DateTime.Now;
                //var res = _EFCoreService.SetRoleMenu(roleMenu);
                var res = await _roleMenuService.Add(roleMenu);
                return new JsonResult(new Result { Data = res, Msg = $"设置{(res ? "成功" : "失败")}", Status = res ? 200 : 204 });
            }
        }

        [HttpGet]
        [Route("GetRoleMenusByRoleId")]
        public async Task<IActionResult> GetRoleMenusByRoleId(string roleId)
        {
            // var roleMenu = _EFCoreService.GetRoleMenuByRoleId(roleId);
            var roleMenu = await _roleMenuService.QueryByWhereAsync(o => o.RoleId == roleId);
            if (roleMenu != null)
            {
                string menuIds = roleMenu.MenuInfoId;
                var res =await _menuInfoService.GetMenus(menuIds);

                return new JsonResult(new Result { Data = res, Msg = "获取成功", Status = 200 });
            }
            return new JsonResult(new Result { Data = null, Msg = "获取失败", Status = 200 });

        }
        #endregion
    }
}