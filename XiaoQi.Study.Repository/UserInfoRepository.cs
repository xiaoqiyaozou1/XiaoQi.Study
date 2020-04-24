using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public IQueryable<UserInfo> GetUserinfoPageInfos<S>(int pageIndex, int pageSize, out int total, Expression<Func<UserInfo, bool>> whereLambda, Expression<Func<UserInfo, S>> orderByLambda, bool isAsc)
        {
            total = _myContext.Set<UserInfo>().Where(whereLambda).Count();

            if (isAsc)
            {
                var res = _myContext.Set<UserInfo>()
                    .Where(whereLambda)
                    .OrderBy<UserInfo, S>(orderByLambda)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();
                var tmp = res.ToList();
                foreach (var item in tmp)
                {
                    string userId = item.UserId;
                    var userRole = _myContext.UserRole_Rs.Where(o => o.UserId == userId).FirstOrDefault();
                    if (userRole != null)
                    {
                        item._RoleInfo = _myContext.RoleInfos.Find(userRole.RoleId);
                    }
                }
                return res;
            }
            else
            {
                var res = _myContext.Set<UserInfo>()
                          .Where(whereLambda)
                          .OrderBy<UserInfo, S>(orderByLambda)
                          .Skip(pageSize * (pageIndex - 1))
                          .Take(pageSize).AsQueryable();

                var tmp = res.ToList();
                foreach (var item in tmp)
                {
                    string userId = item.UserId;
                    var userRole = _myContext.UserRole_Rs.Where(o => o.UserId == userId).FirstOrDefault();
                    if (userRole != null)
                    {
                        item._RoleInfo = _myContext.RoleInfos.Find(userRole.RoleId);
                    }
                }
                return res;
            }
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
