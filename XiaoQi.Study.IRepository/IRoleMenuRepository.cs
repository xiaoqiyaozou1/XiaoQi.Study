using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.IRepository
{
    public interface IRoleMenuRepository : IBaseRepository<RoleMenu_R>
    {
        Task<RoleMenu_R> GetRoleMenusByRoleId(string userId);
    }
}
