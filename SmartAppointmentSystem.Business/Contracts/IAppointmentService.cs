﻿using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IAppointmentService
{
    Task<bool> CreateAppointment(AppointmentRequestDTO requestDTO);
    Task<Appointment> GetAppointmentsById(Guid id);
    Task<bool> DeleteAppointmentById(Guid id);
}
