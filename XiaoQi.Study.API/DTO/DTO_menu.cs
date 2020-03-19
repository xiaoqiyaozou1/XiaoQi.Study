using System.Collections.Generic;

namespace XiaoQi.Study.API
{
    public class DTO_menu
    {
        public string menuInfoId { get; set; }
        public string menuName { get; set; }
        public string menuUrl { get; set; }
        public string fatherId { get; set; }
        public string selfId { get; set; }
        public IEnumerable<DTO_menu> children { get; set; }
    }
}