using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoQi.Study.Model
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string Name { get; set; }

        public string Count { get; set; }
        public string Pwd { get; set; }

        public DateTime SetTime { get; set; }

        public RoleInfo _RoleInfo { get; set; }

    }
}
