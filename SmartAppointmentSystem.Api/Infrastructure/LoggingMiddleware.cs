using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Middlewares
{
    public class RequestLoggingMiddleware(RequestDelegate requestDelegate, ILogger<RequestLoggingMiddleware> logger, ILoggingService loggingService)
    {
        public async Task InvokeAsync(HttpContext context, CancellationToken cancellationToken)
        {
            await requestDelegate(context);
            var logDto = context.ToLogDTO();
            try
            {
                await loggingService.LogAsync(logDto, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Log error while creating.");
            }
        }
    }
}
