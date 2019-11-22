using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UsersService.AspNet
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment hostEnvironment;
        private readonly ILogger<CustomExceptionFilter> logger;

        public CustomExceptionFilter(
            IHostEnvironment hostEnvironment,
            ILogger<CustomExceptionFilter> logger)
        {
            this.hostEnvironment = hostEnvironment;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception == null) return;
            
            var e = context.Exception;
            logger.LogError(e, e.Message);

            var result = new ObjectResult("Please contact support.")
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
                
            if (!hostEnvironment.IsProduction())
            {
                result.Value = e.Message;
            }
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}