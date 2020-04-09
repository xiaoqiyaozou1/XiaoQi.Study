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
    public class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
    {
        private readonly MyContext _myContext;
        public UserInfoRepository(MyContext myContext) : base(myContext)
        {
            _myContext = myContext;
        }
        /// <summary>
        /// 得到用户的信息
        /// </summary>
        /// <param name="count"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<UserInfo> GetUserInfoByCountAndPwdAsync(string count, string pwd)
        {
            //被群里老哥点醒 的 没有异步可以自己写个异步嘛，，，，
            var res = await Task.Run(() => { return _myContext.UserInfos.Where(o => o.Count == count && o.Pwd == pwd).FirstOrDefault(); });
            return res;
        }
    }
}
