using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Business.Extensions;

public static class MapperExtensions
{
    public static User Map(this UserRequestDTO registerRequest)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = registerRequest.Name,
            Email = registerRequest.Email,
            PasswordHash = registerRequest.Password,
            Role = registerRequest.Role,
        };
    }
    public static Appointment Map(this AppointmentRequestDTO createRequest)
    {
        return new Appointment
        {
            Id = Guid.NewGuid(),
            CustomerId = createRequest.CustomerId,
            DateTime = createRequest.DateTime,
            Notes = createRequest.Notes,
            Status = createRequest.Status,
            ProfessionalId = createRequest.ProfessionalId,
        };
    }
}
