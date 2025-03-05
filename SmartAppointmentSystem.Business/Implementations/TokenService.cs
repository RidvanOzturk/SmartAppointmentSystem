using Microsoft.IdentityModel.Tokens;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace SmartAppointmentSystem.Business.Implementations;

public class TokenService : ITokenService
{
    public Task<GenerateTokenResponseDTO> GenerateToken(GenerateTokenRequestDTO request)
    {
        var secretKey = Environment.GetEnvironmentVariable("AppSettings__Secret");
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new Exception("JWT Secret Key not found.");
        }
        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        var dateTimeNow = DateTime.UtcNow;

        JwtSecurityToken jwt = new JwtSecurityToken(
            claims: new List<Claim>
            {
                new Claim("UserId", request.UserId.ToString()),
                new Claim("Name", request.Name),
                new Claim("Mail", request.Mail),
                new Claim(ClaimTypes.Role, request.Role)
            },
            notBefore: dateTimeNow,
            expires: dateTimeNow.AddMinutes(300),
            signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
        );

        string token = new JwtSecurityTokenHandler().WriteToken(jwt);

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("JWT Token cannot be created.");
        }

        return Task.FromResult(new GenerateTokenResponseDTO
        {
            Token = token,
            TokenExpireDate = dateTimeNow.AddMinutes(500)
        });

    }
}
