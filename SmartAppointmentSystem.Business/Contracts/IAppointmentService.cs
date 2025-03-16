using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IAppointmentService
{
    Task CreateAppointmentAsync(AppointmentRequestDTO requestDTO, CancellationToken cancellationToken = default);
    Task<Appointment> GetAppointmentsByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Appointment>> GetUserAppointmentsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<string>> GetAvailableTimeSlotsForDoctorAsync(Guid id, DateTime date, CancellationToken cancellationToken);
    Task DeleteAppointmentByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAppointmentByIdAsync(Guid id, AppointmentRequestDTO appointmentRequestDTO, CancellationToken cancellationToken = default);
    Task<List<Appointment>> GetAllAppointmentsAsync(CancellationToken cancellationToken = default);
    Task<bool> IsAppointmentExistAsync(Guid id, CancellationToken cancellationToken = default);
}
