﻿using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Data;

namespace SmartAppointmentSystem.Business.Implementations;

public class AppointmentService(AppointmentContext appointmentContext) : IAppointmentService
{
}
