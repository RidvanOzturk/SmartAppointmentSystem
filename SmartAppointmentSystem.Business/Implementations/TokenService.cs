using SmartAppointmentSystem.Business.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SmartAppointmentSystem.Data;
using Microsoft.Extensions.Configuration;
using SmartAppointmentSystem.Business.DTOs;
namespace SmartAppointmentSystem.Business.Implementations;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public Task<GenerateTokenResponseDTO> GenerateToken(GenerateTokenRequestDTO request)
    {
        Console.WriteLine(configuration["AppSettings:Secret"]);
        var secretKey = Environment.GetEnvironmentVariable("AppSettings__Secret");
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new Exception("JWT Secret Key not found.");
        }
        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        var dateTimeNow = DateTime.UtcNow;

        JwtSecurityToken jwt = new JwtSecurityToken(
            issuer: configuration["AppSettings:ValidIssuer"],
            audience: configuration["AppSettings:ValidAudience"],
            claims: new List<Claim>
            {
                new Claim("Name", request.Name)
            },
            notBefore: dateTimeNow,
            expires: dateTimeNow.AddMinutes(500),
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
