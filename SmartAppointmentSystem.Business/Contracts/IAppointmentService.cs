using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IAppointmentService
{
    Task<bool> CreateAppointment(AppointmentRequestDTO requestDTO);
    Task<Appointment> GetAppointmentsById(Guid id);
    Task<List<Appointment>> GetUserAppointments(Guid id);
    Task<bool> DeleteAppointmentById(Guid id);
    Task<bool> UpdateAppointmentById(Guid id, AppointmentRequestDTO appointmentRequestDTO);
    Task<List<Appointment>> GetAllAppointments();
}
