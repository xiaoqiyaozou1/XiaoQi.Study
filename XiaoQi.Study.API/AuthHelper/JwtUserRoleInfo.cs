using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XiaoQi.Study.API.AuthHelper
{
    /// <summary>
    /// 用户角色信息实体
    /// </summary>
    public class JwtUserRoleInfo
    {
        public virtual string Role { get; set; }
        public  virtual string Url { get; set; }
    }
}
