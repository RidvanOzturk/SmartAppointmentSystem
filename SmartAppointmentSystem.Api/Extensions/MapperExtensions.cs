using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Api.Extensions;

public static class MapperExtensions
{
    public static User Map(this UserRequestModel value)
    {
        return new User()
        {
            Id = Guid.NewGuid(), // normalde db'de auto increment olucak
            Name = value.Name,
            Mail = value.Email,
            PasswordHash = value.Password // normalde hashlemek lazım
        };
    }
}
