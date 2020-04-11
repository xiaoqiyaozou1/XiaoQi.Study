using System;
using System.Collections.Generic;
using System.Text;
using XiaoQi.Study.IRepository;
using XiaoQi.Study.IService;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.Service
{
    public class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
    {
        public readonly IRoleInfoRepository _roleInfoRepository;
        public RoleInfoService(IRoleInfoRepository roleInfoRepository)
        {
            _roleInfoRepository = roleInfoRepository;
            this._baseRepository = _roleInfoRepository;
        }
        public override void SetBaseRepository()
        {
            this._baseRepository = _roleInfoRepository;
        }
    }
}
