using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface ITokenService
{
    string GenerateToken(TokenRequest request);
    string GenerateRefreshTokenAsync();
}
