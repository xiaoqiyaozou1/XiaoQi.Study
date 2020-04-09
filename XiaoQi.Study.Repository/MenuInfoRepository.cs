using System;
using System.Collections.Generic;
using System.Text;
using XiaoQi.Study.EF;
using XiaoQi.Study.IRepository;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.Repository
{
    public class MenuInfoRepository : BaseRepository<MenuInfo>, IMenuInfoRepository
    {
        private readonly MyContext _myContext;
        public MenuInfoRepository(MyContext myContext) : base(myContext)
        {
            _myContext = myContext;
        }
    }
}
