using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;

namespace SmartAppointmentSystem.Business.Implementations
{
    public class LoggingService(AppointmentContext context) : ILoggingService
    {
        public async Task LogAsync(LogDTO log, CancellationToken cancellationToken)
        {
            var logEntity = log.Map();
            context.Logs.Add(logEntity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
