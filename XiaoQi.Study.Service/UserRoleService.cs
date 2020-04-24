using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XiaoQi.Study.IRepository;
using XiaoQi.Study.IService;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.Service
{
    public class UserRoleService : BaseService<UserRole_R>, IUserRoleService
    {
        public readonly IUserRoleRepository _userRoleReposiroty;
        public UserRoleService(IUserRoleRepository userRoleReposiroty)
        {
            _userRoleReposiroty = userRoleReposiroty;
            this._baseRepository = _userRoleReposiroty;
        }
        public override void SetBaseRepository()
        {
            this._baseRepository = _userRoleReposiroty;
        }
        public async Task<UserRole_R> GetUserRoleByUserId(string userId)
        {
            return await _userRoleReposiroty.GetUserRoleByUserId(userId);
        }
    }
}
