using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Api.Extensions;

public static class MapperExtensions
{
    public static RegisterRequestDTO Map(this UserRequestModel value)
    {
        return new RegisterRequestDTO
        {
            Name = value.Name,
            Email = value.Email,
            Password = value.Password,
            Role = value.Role,
        };
    }
}
