using SimpleECommerce.InfraStructure.Logging;
using System.Net;

namespace SimpleECommerce.Middleware
{
    public class ExceptionHandlingiddleware
    {
        private RequestDelegate _next;
        private IAppLogger<ExceptionHandlingiddleware> _logger;

        public ExceptionHandlingiddleware(RequestDelegate next, IAppLogger<ExceptionHandlingiddleware> logger)
        { 
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e) 
            {
                string message = "サーバー内部でエラーが発生しました";

                _logger.Error
                (
                    e,
                    message,
                    new
                    {
                        TraceId = context.TraceIdentifier,
                        User = context.User?.Identity?.Name,
                        Path = context.Request.Path!,
                        Method = context.Request.Method!,
                        Exception = e
                    }
                );

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync( message );
            }
        }
    }
}
