using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Data.Repositories.Contracts;

namespace SmartAppointmentSystem.Business.Implementations;

public class AppointmentBusiness(IAppointmentRepository appointmentRepository) : IAppointmentBusiness
{
}
