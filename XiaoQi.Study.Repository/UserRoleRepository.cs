using System;
using System.Collections.Generic;
using System.Text;
using XiaoQi.Study.EF;
using XiaoQi.Study.IRepository;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.Repository
{
    public class UserRoleRepository : BaseRepository<UserRole_R>, IUserRoleRepository
    {
        private readonly MyContext _myContext;
        public UserRoleRepository(MyContext myContext) : base(myContext)
        {
            _myContext = myContext;
        }
    }
}
