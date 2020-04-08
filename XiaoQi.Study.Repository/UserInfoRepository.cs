using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XiaoQi.Study.EF;
using XiaoQi.Study.IRepository;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.Repository
{
    public class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
    {
        private readonly MyContext _myContext;
        public UserInfoRepository(MyContext myContext):base(myContext)
        {
            _myContext = myContext;
        }

        public Task<UserInfo> GetUserInfoByCountAndPwd(string count, string pwd)
        {
            throw new NotImplementedException();
        }
    }
}
