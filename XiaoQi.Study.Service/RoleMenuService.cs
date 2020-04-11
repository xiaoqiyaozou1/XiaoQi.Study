using System;
using System.Collections.Generic;
using System.Text;
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

        public override void SetBaseRepository()
        {
            this._baseRepository = _roleMenuRepository;
        }
    }
}
