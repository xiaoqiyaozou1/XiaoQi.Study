using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using XiaoQi.Study.IRepository;
using XiaoQi.Study.IService;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.Service
{
    public class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        public readonly IUserInfoRepository _userInfoRepository;
        public UserInfoService(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
            this._baseRepository = _userInfoRepository;
        }

        public IQueryable<UserInfo> GetUserInfoPageInfos<S>(int pageIndex, int pageSize, out int total, Expression<Func<UserInfo, bool>> whereLambda, Expression<Func<UserInfo, S>> orderByLambda, bool isAsc)
        {
            return this._userInfoRepository.GetUserinfoPageInfos(pageIndex, pageSize, out total, whereLambda, orderByLambda, isAsc);
        }
        public override void SetBaseRepository()
        {
            this._baseRepository = _userInfoRepository;
        }
    }
}
