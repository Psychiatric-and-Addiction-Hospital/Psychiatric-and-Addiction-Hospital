namespace Psychiatric_and_Addiction_Hospital.Extesion
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionExtension(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionExtension>();
        }
    }
}
