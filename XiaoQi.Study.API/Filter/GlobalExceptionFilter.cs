using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiaoQi.Study.API.Common;

namespace XiaoQi.Study.API.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(IWebHostEnvironment env, ILogger<GlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            string msg = string.Empty;
            if (_env.IsDevelopment())
            {
                msg = context.Exception.StackTrace;
                LogHelper.Error(msg);
            }
            else
            {
                msg = context.Exception.Message;//错误信息
            }
            throw new NotImplementedException();
        }
    }
}
