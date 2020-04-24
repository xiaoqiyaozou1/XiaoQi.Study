using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XiaoQi.Study.IRepository;
using XiaoQi.Study.IService;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.Service
{
    public class RoleMenuService : BaseService<RoleMenu_R>, IRoleMenuService
    {
        public readonly IRoleMenuRepository _roleMenuRepository;
        public RoleMenuService(IRoleMenuRepository roleMenuRepository)
        {
            _roleMenuRepository = roleMenuRepository;
            this._baseRepository = _roleMenuRepository;
        }

        public async Task<RoleMenu_R> GetRoleMenusByRoleId(string roleId)
        {
            return await _roleMenuRepository.GetRoleMenusByRoleId(roleId);
        }

        public override void SetBaseRepository()
        {
            this._baseRepository = _roleMenuRepository;
        }
    }
}
