using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XiaoQi.Study.API.AuthHelper;
using XiaoQi.Study.EF;

namespace XiaoQi.Study.API
{
    public interface IEFCoreService
    {
        #region 用户信息管理的相关代码
        /// <summary>
        /// 查询所有用户信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserInfo> GetAll();

        /// <summary>
        /// 根据账户查询用户信息
        /// </summary>
        /// <param name="count"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        UserInfo GetByCountPwd(string count, string pwd);

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        bool AddUserInfo(UserInfo userInfo);

        /// <summary>
        /// 更改用户数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        bool UpdateUserInfos(UserInfo userInfo);

        /// <summary>
        /// 根据用户ID删除数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool DeleteUserInfo(string userId);
        IEnumerable<UserInfo> GetUserInfos();

        UserInfo GetUserInfoByUserId(string userId);

        IQueryable<UserInfo> GetUserPageEntities(int pageSize, int pageIndex, out int total, bool isAsc);
        #endregion

        #region 角色管理的相关代码
        /// <summary>
        /// 分页查询角色信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        IQueryable<RoleInfo> GetRolPageEnties(int pageSize, int pageIndex, out int total, bool isAsc);
        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        bool AddRoleInfo(RoleInfo roleInfo);
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        bool DeleteRoleInfo(string roleId);
        /// <summary>
        /// 更改角色信息
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        bool UpdateRoleInfo(RoleInfo roleInfo);
        /// <summary>
        /// 根据角色ID获取角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        RoleInfo GetRoleInfoByID(string roleId);

        IEnumerable<RoleInfo> GetRoleInfo();
        #endregion

        #region 菜单管理相关代码
        IEnumerable<MenuInfo> GetAllMenuInfo();
        bool AddMenuInfo(MenuInfo menuInfo);
        bool UpdateMenuInfo(MenuInfo menuInfo);
        bool DeleteMenuInfo(string menuInfoId);
        MenuInfo GetMenuInfoById(string menuInfoId);

        IEnumerable<DTO_menu> GetMenuTree(IEnumerable<MenuInfo> menuInfo, string fatherId);

        #endregion

        #region 用户角色相关
        bool SetUserRole(UserRole_R userRole);

        bool UpdateUserRole(UserRole_R userRole);

        UserRole_R GetUserRoleById(string userRoleId);

        bool DeleteUserRoleById(string userRoleId);

        UserRole_R GetUserRoleByUserId(string userId);

        string GetRoleInfos(string userId);
        #endregion

        #region 角色菜单相关
        bool SetRoleMenu(RoleMenu_R roleMenu);
        bool UpdateRoleMenu(RoleMenu_R roleMenu);
        RoleMenu_R GetRoleMenuByRoleId(string roleId);

        bool DeleteRoleMenuById(string roleMenuId);

        IEnumerable<MenuInfo> GetRoleMenus(string menuIds);

        List<JwtUserRoleInfo> GetAllRoleMenu();
        #endregion


    }
}
