using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoQi.Study.Model.DTO
{
    public class MenuDTO
    {
        public string menuInfoId { get; set; }
        public string menuName { get; set; }
        public string menuUrl { get; set; }
        public string fatherId { get; set; }
        public string selfId { get; set; }
        public List<MenuDTO> children { get; set; }
    }
}
