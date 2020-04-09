using System;
using System.Collections.Generic;
using System.Text;
using XiaoQi.Study.EF;
using XiaoQi.Study.IRepository;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.Repository
{
    public class MenuButtonRepository : BaseRepository<MenuButton_R>, IMenuButtonRepository
    {
        private readonly MyContext _myContext;
        public MenuButtonRepository(MyContext myContext) : base(myContext)
        {
            _myContext = myContext;
        }
    }
}
