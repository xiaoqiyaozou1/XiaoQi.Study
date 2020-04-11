using System;
using System.Collections.Generic;
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
        public override void SetBaseRepository()
        {
            this._baseRepository = _userInfoRepository;
        }
    }
}
