using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Data.Repositories.Contracts;

namespace SmartAppointmentSystem.Business.Implementations;

public class AppointmentService(IAppointmentRepository appointmentRepository) : IAppointmentService
{
}
