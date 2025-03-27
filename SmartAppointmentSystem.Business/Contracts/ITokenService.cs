using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface ITokenService
{
    public Task<GenerateTokenResponseDTO> GenerateToken(GenerateTokenRequestDTO request);
    Task<string> GenerateRefreshTokenAsync();
}
