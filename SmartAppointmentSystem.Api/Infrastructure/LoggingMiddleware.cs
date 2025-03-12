using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        //Singleton problem will be ask.
        //primary constructor not allowed
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            var logDto = context.ToLogDTO();
            try
            {
                var scopedLoggingService = context.RequestServices.GetRequiredService<ILoggingService>();
                await scopedLoggingService.LogAsync(logDto, context.RequestAborted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Log error while creating.");
            }
        }
    }
}
