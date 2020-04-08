using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoQi.Study.Model
{
    public class MenuInfo
    {
        public string MenuInfoId { get; set; }
        public string SelfId { get; set; }
        public string FatherId { get; set; }
        public string MenuUrl { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public string MenyDes { get; set; }
        public DateTime SetTime { get; set; }
    }
}
