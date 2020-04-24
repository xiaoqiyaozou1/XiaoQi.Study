using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.IRepository
{
    public interface IUserInfoRepository : IBaseRepository<UserInfo>
    {
        Task<UserInfo> GetUserInfoByCountAndPwdAsync(string count, string pwd);

        IQueryable<UserInfo> GetUserinfoPageInfos<S>(int pageIndex, int pageSize, out int total, Expression<Func<UserInfo, bool>> whereLambda, Expression<Func<UserInfo, S>> orderByLambda,
                                       bool isAsc);
    }
}
