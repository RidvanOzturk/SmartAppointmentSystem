using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace SmartAppointmentSystem.Business.Implementations;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string GenerateToken(TokenRequest request)
    {
        var secretKey = Environment.GetEnvironmentVariable("AppSettings__Secret");
        int tokenExpiryMinutes = configuration.GetValue<int>("TokenSettings:ExpiresInMinutes");
        var issuer = configuration.GetValue<string>("TokenSettings:Issuer");
        var audience = configuration.GetValue<string>("TokenSettings:Audience");

        ArgumentException.ThrowIfNullOrWhiteSpace(secretKey);
        ArgumentException.ThrowIfNullOrWhiteSpace(issuer);
        ArgumentException.ThrowIfNullOrWhiteSpace(audience);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(tokenExpiryMinutes);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

        var claims = new List<Claim>
        {
            new Claim("UserId", request.UserId.ToString()),
            new Claim("Name", request.Name),
            new Claim("Email", request.Mail)
        };

        var jwt = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(tokenExpiryMinutes),
            signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        ArgumentException.ThrowIfNullOrWhiteSpace(token);

        return token;
    }
    //TODO
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
