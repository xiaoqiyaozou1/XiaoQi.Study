using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.IRepository
{
    public interface IUserRoleRepository : IBaseRepository<UserRole_R>
    {
        Task<UserRole_R> GetUserRoleByUserId(string userId);
    }
}
