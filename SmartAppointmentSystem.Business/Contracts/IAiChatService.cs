using SmartAppointmentSystem.Business.DTOs.AI;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IAiChatService
{
    Task<string> ChatAsync(string prompt, CancellationToken cancellationToken = default);

    Task<AppointmentSearchIntent> ParseAppointmentSearchAsync(string prompt, CancellationToken cancellationToken = default);
}