using System;
using System.Collections.Generic;
using System.Text;
using XiaoQi.Study.EF;
using XiaoQi.Study.IRepository;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.Repository
{
    public class RoleMenuRepository : BaseRepository<RoleMenu_R>, IRoleMenuRepository
    {
        private readonly MyContext _myContext;
        public RoleMenuRepository(MyContext myContext) : base(myContext)
        {
            _myContext = myContext;
        }
    }
}
