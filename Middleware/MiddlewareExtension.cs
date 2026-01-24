namespace SimpleECommerce.Middleware
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseGlobalExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionHandlingiddleware>();
            return builder;
        }
    }
}
