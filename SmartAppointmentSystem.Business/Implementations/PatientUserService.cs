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

public class PatientUserService(AppointmentContext context, ITokenService tokenService) : IPatientUserService
{
    public async Task RegisterAsync(PatientUserRequestDTO requestDTO, CancellationToken cancellationToken)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password);
        var patient = requestDTO.Map();
        patient.PasswordHash = hashedPassword;
        await context.Patients.AddAsync(patient, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserResponseModel> LoginUserAsync(PatientUserRequestDTO request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Password))
        {
            throw new ArgumentNullException(nameof(request));
        }

        var patient = await context.Patients.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
        if (patient == null || !BCrypt.Net.BCrypt.Verify(request.Password, patient.PasswordHash))
        {
            throw new UnauthorizedAccessException("Kullanıcı adı veya şifre yanlış.");
        }

        var generatedToken = await tokenService.GenerateToken(new GenerateTokenRequestDTO { UserId = patient.Id, Name = patient.Name, Mail = patient.Email, Role = "Patient" });
        return new UserResponseModel
        {
            AccessTokenExpireDate = generatedToken.TokenExpireDate,
            AuthenticateResult = true,
            AuthToken = generatedToken.Token
        };
    }
    public async Task<List<Patient>> GetUsersAsync(CancellationToken cancellationToken)
    {
        return await context.Patients
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    public async Task<Patient> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    public async Task DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var patient = await context.Patients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        context.Patients.Remove(patient);
        await context.SaveChangesAsync(cancellationToken);
    }
}
