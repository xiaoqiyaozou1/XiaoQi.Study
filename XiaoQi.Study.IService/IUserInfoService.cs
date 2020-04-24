using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.IService
{
    public interface IUserInfoService : IBaseService<UserInfo>
    {
        IQueryable<UserInfo> GetUserInfoPageInfos<S>(int pageIndex, int pageSize, out int total, Expression<Func<UserInfo, bool>> whereLambda, Expression<Func<UserInfo, S>> orderByLambda,
                                                 bool isAsc);
    }
}
