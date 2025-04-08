using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface ITokenService
{
    public string GenerateToken(TokenRequest request);
    Task<string> GenerateRefreshTokenAsync();
}
