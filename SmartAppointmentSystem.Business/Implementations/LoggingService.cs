using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class LoggingService(AppointmentContext context) : ILoggingService
{
    public async Task LogAsync(LogDTO log, CancellationToken cancellationToken)
    {
        var logEntity = log.Map();
        context.Logs.Add(logEntity);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task LogErrorAsync(Exception ex, CancellationToken cancellationToken)
    {
        var errorLog = new LogError
        {
            Id = Guid.NewGuid(),
            ExceptionMessage = ex.ToString(),
            CreatedAt = DateTime.UtcNow
        };

        context.LogErrors.Add(errorLog);
        await context.SaveChangesAsync(cancellationToken);
    }
}
