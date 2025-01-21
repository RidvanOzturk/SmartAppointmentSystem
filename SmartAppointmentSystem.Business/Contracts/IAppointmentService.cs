using SmartAppointmentSystem.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IAppointmentService
{
    Task<bool> CommitAsync();
    Task<bool> CreateAppointment(AppointmentRequestDTO requestDTO);
}
