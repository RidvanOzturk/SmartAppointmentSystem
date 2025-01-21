using SmartAppointmentSystem.Business.DTOs;

namespace SmartAppointmentSystem.Business.Contracts;

public interface ITokenService
{
    public Task<GenerateTokenResponseDTO> GenerateToken(GenerateTokenRequestDTO request);

}
