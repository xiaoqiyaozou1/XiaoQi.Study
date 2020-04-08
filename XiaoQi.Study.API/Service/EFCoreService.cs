
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XiaoQi.Study.API.AuthHelper;
using XiaoQi.Study.EF;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.API
{
    public class EFCoreService : IEFCoreService
    {
        private readonly MyContext _myContext;
        #region 用户信息相关
        public EFCoreService(MyContext myContext)
        {
            _myContext = myContext;
        }

        /// <summary>
        /// 查询所有用户信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserInfo> GetUserInfos()
        {
            var userInfo = _myContext.UserInfos.ToList();
            foreach (var item in userInfo)
            {
                var tmp = _myContext.UserRole_Rs
                          .Where(o => o.UserId == item.UserId).FirstOrDefault();
                if (tmp != null)
                    item._RoleInfo = _myContext.RoleInfos
                        .Where(o => o.RoleId == tmp.RoleId).FirstOrDefault();

            }

            return userInfo;
        }
        /// <summary>
        /// 根据账户查询用户信息
        /// </summary>
        /// <param name="count"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public UserInfo GetByCountPwd(string count, string pwd)
        {
            var data = (from o in _myContext.UserInfos
                        where o.Count == count && o.Pwd == pwd
                        select o).FirstOrDefault();
            return data;
        }
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool AddUserInfo(UserInfo userInfo)
        {
            _myContext.UserInfos.Add(userInfo);
            return _myContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 更改用户数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool UpdateUserInfos(UserInfo userInfo)
        {
            var tmpData = _myContext.Set<UserInfo>().FirstOrDefault(o => o.UserId == userInfo.UserId);
            tmpData.Name = userInfo.Name;
            tmpData.Pwd = userInfo.Pwd;
            tmpData.SetTime = System.DateTime.Now;
            tmpData.Count = userInfo.Count;
            return _myContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 根据用户ID删除数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUserInfo(string userId)
        {
            var tmpData = _myContext.Set<UserInfo>().FirstOrDefault(o => o.UserId == userId);
            _myContext.UserInfos.Remove(tmpData);
            return _myContext.SaveChanges() > 0;
        }

        public IEnumerable<UserInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 本来应该在类似于DAL层编写
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<UserInfo> GetUserPageEntities(int pageSize, int pageIndex, out int total, bool isAsc)
        {
            total = _myContext.Set<UserInfo>().Count();
            var userInfos = _myContext.Set<UserInfo>()
                             .Skip(pageSize * (pageIndex - 1))
                             .Take(pageSize).AsQueryable();
            foreach (var item in userInfos)
            {
                string userId = item.UserId;
                var userRole = _myContext.UserRole_Rs.Where(o => o.UserId == userId).FirstOrDefault();
                if (userRole != null)
                {
                    item._RoleInfo = _myContext.RoleInfos.Find(userRole.RoleId);
                }

            }
            return userInfos;
            //if (isAsc)
            //{
            //    var temp = _myContext.Set<UserInfo>()
            //                 .Skip(pageSize * (pageIndex - 1))
            //                 .Take(pageSize).AsQueryable();

            //    return temp;
            //}
            //else
            //{
            //    var temp = _myContext.Set<UserInfo>()
            //                   .Skip(pageSize * (pageIndex - 1))
            //                   .Take(pageSize).AsQueryable();

            //    return temp;

            //}
        }

        public UserInfo GetUserInfoByUserId(string userId)
        {
            return _myContext.UserInfos.Find(userId);

        }

        #endregion
        #region RoleInfo_CRUD

        #endregion

        public IQueryable<RoleInfo> GetRolPageEnties(int pageSize, int pageIndex, out int total, bool isAsc)
        {
            total = _myContext.Set<RoleInfo>().Count();

            if (isAsc)
            {
                var temp = _myContext.Set<RoleInfo>()
                             .Skip(pageSize * (pageIndex - 1))
                             .Take(pageSize).AsQueryable();
                return temp;
            }
            else
            {
                var temp = _myContext.Set<RoleInfo>()
                               .Skip(pageSize * (pageIndex - 1))
                               .Take(pageSize).AsQueryable();
                return temp;

            }
        }

        public bool AddRoleInfo(RoleInfo roleInfo)
        {

            _myContext.RoleInfos.Add(roleInfo);
            return _myContext.SaveChanges() > 0;
        }

        public bool DeleteRoleInfo(string roleId)
        {
            var delData = _myContext.Set<RoleInfo>().FirstOrDefault(o => o.RoleId == roleId);
            _myContext.RoleInfos.Remove(delData);
            return _myContext.SaveChanges() > 0;
        }

        public bool UpdateRoleInfo(RoleInfo roleInfo)
        {
            var upData = _myContext.Set<RoleInfo>().FirstOrDefault(o => o.RoleId == roleInfo.RoleId);
            upData.Role = roleInfo.Role;
            upData.Des = roleInfo.Des;
            upData.SetTime = roleInfo.SetTime;
            return _myContext.SaveChanges() > 0;
        }

        public RoleInfo GetRoleInfoByID(string roleId)
        {
            return _myContext.RoleInfos.Find(roleId);
        }

        public IEnumerable<MenuInfo> GetAllMenuInfo()
        {
            return _myContext.MenuInfos.ToList();
        }

        public bool AddMenuInfo(MenuInfo menuInfo)
        {
            _myContext.MenuInfos.Add(menuInfo);
            return _myContext.SaveChanges() > 0;
        }

        public bool UpdateMenuInfo(MenuInfo menuInfo)
        {
            _myContext.MenuInfos.Update(menuInfo);
            return _myContext.SaveChanges() > 0;
        }

        public bool DeleteMenuInfo(string menuInfoId)
        {
            var tmpData = _myContext.MenuInfos.Find(menuInfoId);
            _myContext.MenuInfos.Remove(tmpData);
            return _myContext.SaveChanges() > 0;
        }

        public MenuInfo GetMenuInfoById(string menuInfoId)
        {
            return _myContext.MenuInfos.Find(menuInfoId);
        }

        public IEnumerable<DTO_menu> GetMenuTree(IEnumerable<MenuInfo> menuInfos, string fatherId)
        {
            List<DTO_menu> data = new List<DTO_menu>();

            //拿到所有的父类
            var tmpData = menuInfos.Where(o => o.FatherId == fatherId).OrderBy(o => o.SetTime);

            if (tmpData.Count() > 0)
            {
                foreach (var item in tmpData)
                {
                    var newObj = new DTO_menu
                    {
                        menuInfoId = item.MenuInfoId,
                        menuName = item.MenuName,
                        selfId = item.SelfId,
                        fatherId = item.FatherId,
                        menuUrl = item.MenuUrl,

                    };
                    //得到该父类下所有的子类
                    var childData = menuInfos.Where(o => o.FatherId == item.SelfId);

                    //如果有子类则进行如下操作
                    if (childData.Count() > 0)
                    {
                        newObj.children = GetMenuTree(childData, item.SelfId);
                        data.Add(newObj);
                    }
                    else
                    {
                        data.Add(newObj);

                    }
                }
            }
            Console.WriteLine(data.Count());
            return data;
        }

        public bool SetUserRole(UserRole_R userRole)
        {
            _myContext.UserRole_Rs.Add(userRole);
            return _myContext.SaveChanges() > 0;
        }

        public bool UpdateUserRole(UserRole_R userRole)
        {
            _myContext.UserRole_Rs.Update(userRole);
            return _myContext.SaveChanges() > 0;
        }

        public UserRole_R GetUserRoleById(string userRoleId)
        {
            var data = _myContext.UserRole_Rs.Find(userRoleId);
            return data;
        }

        public bool DeleteUserRoleById(string userRoleId)
        {
            var userRole = GetUserRoleById(userRoleId);
            _myContext.UserRole_Rs.Remove(userRole);
            return _myContext.SaveChanges() > 0;
        }

        public IEnumerable<RoleInfo> GetRoleInfo()
        {
            var roleInfo = _myContext.RoleInfos.ToList();
            return roleInfo;
        }

        public UserRole_R GetUserRoleByUserId(string userId)
        {
            var userRole = _myContext.UserRole_Rs.Where(o => o.UserId == userId).FirstOrDefault();
            return userRole;
        }

        public bool SetRoleMenu(RoleMenu_R roleMenu)
        {
            _myContext.RoleMenu_Rs.Add(roleMenu);
            return _myContext.SaveChanges() > 0;
        }

        public bool UpdateRoleMenu(RoleMenu_R roleMenu)
        {
            _myContext.RoleMenu_Rs.Update(roleMenu);
            return _myContext.SaveChanges() > 0;
        }

        public RoleMenu_R GetRoleMenuByRoleId(string roleId)
        {
            var data = _myContext.RoleMenu_Rs.Where(o => o.RoleId == roleId).FirstOrDefault();
            return data;
        }

        public bool DeleteRoleMenuById(string roleMenuId)
        {
            var data = _myContext.RoleMenu_Rs.Find(roleMenuId);
            _myContext.RoleMenu_Rs.Remove(data);
            return _myContext.SaveChanges() > 0;
        }

        public IEnumerable<MenuInfo> GetRoleMenus(string menuIds)
        {
            List<MenuInfo> menuInfos = new List<MenuInfo>();
            string[] arr = menuIds.Split(',');
            if (arr.Length>0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    var tmpData = _myContext.MenuInfos.Find(arr[i]);
                    if (tmpData!=null)
                    {
                        menuInfos.Add(tmpData);
                    }
                }
            }
            return menuInfos;
        }

        public List<JwtUserRoleInfo> GetAllRoleMenu()
        {
            var roleMenu = _myContext.RoleMenu_Rs.ToList();

            var userRoleInfo = new List<JwtUserRoleInfo>();
            foreach (var item in roleMenu)
            {
                var role = _myContext.RoleInfos.Find(item.RoleId).Role;

                var menuInfoIds = item.MenuInfoId.Split(',');
                if (menuInfoIds.Length>0)
                {
                    for (int i = 0; i < menuInfoIds.Length; i++)
                    {
                        var url = _myContext.MenuInfos.Find(menuInfoIds[i]).MenuUrl;
                        userRoleInfo.Add(new JwtUserRoleInfo
                        {
                            Role = role,
                            Url = url
                        });
                    }
                }
         
            
            }
            return userRoleInfo;
        }

        public string GetRoleInfos(string userId)
        {
            var roleIds = _myContext.UserRole_Rs.Where(o => o.UserId == userId).Select(o => o.RoleId).ToList();
     
            StringBuilder sb = new StringBuilder();
            foreach (var item in roleIds)
            {
                var role = _myContext.RoleInfos.Where(o => o.RoleId == item).Select(o => o.Role).FirstOrDefault();
                sb.Append(role);
                sb.Append(",");
            }
            return sb.ToString();
    
        }
    }
}
