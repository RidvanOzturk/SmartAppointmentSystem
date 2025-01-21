using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Api.Extensions;

public static class MapperExtensions
{
    public static UserRequestDTO Map(this UserRequestModel value)
    {
        return new UserRequestDTO
        {
            Name = value.Name,
            Email = value.Email,
            Password = value.Password,
            Role = value.Role,
        };
    }
    public static AppointmentRequestDTO Map(this AppointmentRequestModel value)
    {
        return new AppointmentRequestDTO
        {
            DateTime = value.DateTime,
            Notes = value.Notes,
            Status = value.Status,
            CustomerId = value.CustomerId,
            ProfessionalId = value.ProfessionalId,
        };
    }
}
