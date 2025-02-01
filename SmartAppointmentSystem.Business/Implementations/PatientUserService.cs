using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Business.Extensions;
using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Data.Entities;
using System.Linq;
using System.Collections.Generic;
using static Azure.Core.HttpHeader;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
namespace SmartAppointmentSystem.Business.Implementations;

public class PatientUserService(AppointmentContext context, IConfiguration configuration, ITokenService tokenService) : IPatientUserService
{
    public async Task<bool> RegisterAsync(PatientUserRequestDTO requestDTO)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password);
        var user = requestDTO.Map();
        user.PasswordHash = hashedPassword;

        await context.Patients.AddAsync(user);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }

    public async Task<PatientUserResponseModel> LoginUserAsync(PatientUserRequestDTO request)
    {
        if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Password))
        {
            throw new ArgumentNullException(nameof(request));
        }

        var user = await context.Patients.FirstOrDefaultAsync(x => x.Name == request.Name);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Kullanıcı adı veya şifre yanlış.");
        }

        var generatedToken = await tokenService.GenerateToken(new GenerateTokenRequestDTO { UserId = user.Id, Name = user.Name, Mail = user.Email });
        return new PatientUserResponseModel
        {
            AccessTokenExpireDate = generatedToken.TokenExpireDate,
            AuthenticateResult = true,
            AuthToken = generatedToken.Token
        };
    }
    public async Task<List<Patient>> GetUsersAsync()
    {
        return await context.Patients.AsNoTracking().ToListAsync();
    }
    public async Task<Patient> GetUserByIdAsync(Guid id)
    {
        return await context.Patients.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<bool> DeleteUserById(Guid id)
    {
        var user = await context.Patients.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
            throw new Exception("Kullanıcı bulunamadı.");
        }

        context.Patients.Remove(user);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
}
