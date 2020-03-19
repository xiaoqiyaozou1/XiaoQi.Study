using System.Collections.Generic;
using XiaoQi.Study.EF;

namespace XiaoQi.Study.API.Controllers
{
    internal class Result
    {
        public object Data { get; set; }
        public string Msg { get; set; }
        public int Status { get; set; }
    }
}