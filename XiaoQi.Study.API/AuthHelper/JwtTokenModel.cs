using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XiaoQi.Study.API.AuthHelper
{
    /// <summary>
    /// 令牌中的内容
    /// </summary>
    public class JwtTokenModel
    {
        public string Uid { get; set; }
        public string Roles { get; set; }
    }
}
