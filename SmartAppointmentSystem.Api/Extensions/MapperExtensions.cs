using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.DTOs;

namespace SmartAppointmentSystem.Api.Extensions
{
    public static class MapperExtensions
    {
        public static RegisterRequestDTO RegisterMap(this RegisterRequestModel requestModel)
        {
            return new RegisterRequestDTO
            {
                name = requestModel.name,
                mail = requestModel.mail,
                password = requestModel.password
            };
        }
    }
}
