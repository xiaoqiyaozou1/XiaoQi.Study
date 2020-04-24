using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<UserRole_R> GetUserRoleByUserId(string userId)
        {
            var res = await Task.Run(() => { return _myContext.Set<UserRole_R>().Where(o => o.UserId == userId).FirstOrDefault(); });
            return res;
        }
    }
}
