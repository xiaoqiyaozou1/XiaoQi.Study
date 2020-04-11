using System;
using System.Collections.Generic;
using System.Text;
using XiaoQi.Study.IRepository;
using XiaoQi.Study.IService;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.Service
{
    public class MenuButtonService : BaseService<MenuButton_R>, IMenuButtonService
    {
        public readonly IMenuButtonRepository _menuButtonRepository;
        public MenuButtonService(IMenuButtonRepository menuButtonRepository)
        {
            _menuButtonRepository = menuButtonRepository;
            this._baseRepository = _menuButtonRepository;
        }
        public override void SetBaseRepository()
        {
            this._baseRepository = _menuButtonRepository;
        }
    }
}
