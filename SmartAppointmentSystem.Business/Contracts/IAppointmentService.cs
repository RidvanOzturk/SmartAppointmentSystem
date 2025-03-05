using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IAppointmentService
{
    Task CreateAppointmentAsync(AppointmentRequestDTO requestDTO, CancellationToken cancellationToken);
    Task<Appointment> GetAppointmentsByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Appointment>> GetUserAppointmentsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> DeleteAppointmentByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateAppointmentByIdAsync(Guid id, AppointmentRequestDTO appointmentRequestDTO, CancellationToken cancellationToken);
    Task<List<Appointment>> GetAllAppointmentsAsync(CancellationToken cancellationToken);
}
