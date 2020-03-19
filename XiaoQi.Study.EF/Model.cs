using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoQi.Study.EF
{
    /// <summary>
    /// userInfo
    /// </summary>
    public class UserInfo
    {
        public string UserId { get; set; }
        public string Name { get; set; }

        public string Count { get; set; }
        public string Pwd { get; set; }

        public DateTime SetTime { get; set; }

        public RoleInfo _RoleInfo { get; set; }

    }
    /// <summary>
    /// userRoleInfo
    /// </summary>
    public class UserRole_R
    {
        public string UserRoleId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public DateTime SetTime { get; set; }
    }
    /// <summary>
    /// RoleInfo
    /// </summary>
    public class RoleInfo
    {
        public string RoleId { get; set; }
        public string Role { get; set; }

        public string Des { get; set; }
        public DateTime SetTime { get; set; }
    }

    public class RoleMenu_R
    {
        public string RoleMenuId { get; set; }
        public string RoleId { get; set; }
        public string MenuInfoId { get; set; }
        public DateTime SetTime { get; set; }
    }

    /// <summary>
    /// MenuInfo
    /// </summary>
    public class MenuInfo
    {
        public string MenuInfoId { get; set; }
        public string SelfId { get; set; }
        public string FatherId { get; set; }
        public string MenuUrl { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public string MenyDes { get; set; }
        public DateTime SetTime { get; set; }
    }
    public class MenuButton_R
    {
        public string MenuButtonId { get; set; }
        public string MenuInfoId { get; set; }
        public string ButtonId { get; set; }
        public DateTime SetTime { get; set; }
    }
    public class ButtonInfo
    {
        public string ButtonId { get; set; }
        public string ButtonName { get; set; }
        public string ButtonIcon { get; set; }
        public string ButtonDes { get; set; }
        public DateTime SetTime { get; set; }
    }
}
