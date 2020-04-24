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
    public class RoleMenuRepository : BaseRepository<RoleMenu_R>, IRoleMenuRepository
    {
        private readonly MyContext _myContext;
        public RoleMenuRepository(MyContext myContext) : base(myContext)
        {
            _myContext = myContext;
        }
        public async Task<RoleMenu_R> GetRoleMenusByRoleId(string roleId)
        {
            var res = await Task.Run(() => { return _myContext.Set<RoleMenu_R>().Where(o => o.RoleId == roleId).FirstOrDefault(); });
            return res;
        }
    }
}
