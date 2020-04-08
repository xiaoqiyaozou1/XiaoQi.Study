using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoQi.Study.Model
{
    public class PageModel
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        public int Total { get; set; }
    }
}
