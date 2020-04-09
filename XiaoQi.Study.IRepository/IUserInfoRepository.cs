using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.IRepository
{
    public interface IUserInfoRepository : IBaseRepository<UserInfo>
    {
        Task<UserInfo> GetUserInfoByCountAndPwdAsync(string count, string pwd);
    }
}
