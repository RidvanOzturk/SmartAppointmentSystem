namespace SmartAppointmentSystem.Business.Contracts;

public interface IAiChatService
{
    Task<string> ChatAsync(string prompt, CancellationToken cancellationToken = default);
}
