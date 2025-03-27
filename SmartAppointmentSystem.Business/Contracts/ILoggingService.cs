using SmartAppointmentSystem.Business.DTOs;

namespace SmartAppointmentSystem.Business.Contracts;

public interface ILoggingService
{
    Task LogAsync(LogDTO log, CancellationToken cancellationToken = default);
    Task LogErrorAsync(Exception ex, CancellationToken cancellationToken);
}