using System;
using System.Collections.Generic;
using System.Text;
using XiaoQi.Study.IRepository;
using XiaoQi.Study.IService;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.Service
{
    public class MenuInfoService : BaseService<MenuInfo>, IMenuInfoService
    {
        private readonly IMenuInfoRepository _mneuInfoRepository;
        public MenuInfoService(IMenuInfoRepository menuInfoRepository)
        {
            _mneuInfoRepository = menuInfoRepository;
            this._baseRepository = _mneuInfoRepository;
        }
        public override void SetBaseRepository()
        {
            this._baseRepository = _mneuInfoRepository;
        }
    }
}
