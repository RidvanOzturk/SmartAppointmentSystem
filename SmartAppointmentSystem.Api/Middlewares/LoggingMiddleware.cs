using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Middlewares;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context, ILoggingService loggingService)
    {
        try
        {
            var requestBody = await context.Request.GetRequestBodyAsync();

            var originalBodyStream = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync(context.RequestAborted);
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            await responseBodyStream.CopyToAsync(originalBodyStream);

            var logDto = context.ToLogDTO(requestBody, responseBody);
            await loggingService.LogAsync(logDto, context.RequestAborted);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Log error while creating.");
            await loggingService.LogErrorAsync(ex, context.RequestAborted);
        }
    }
}
