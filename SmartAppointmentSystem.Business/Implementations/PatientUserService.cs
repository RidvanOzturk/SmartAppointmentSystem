﻿using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;
namespace SmartAppointmentSystem.Business.Implementations;

public class PatientUserService(AppointmentContext context, ITokenService tokenService) : IPatientUserService
{
    public async Task RegisterAsync(PatientUserRequestDTO requestDTO, CancellationToken cancellationToken)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password);
        var patient = requestDTO.Map();
        patient.PasswordHash = hashedPassword;
        await context.Patients
            .AddAsync(patient, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserResponseModel> LoginUserAsync(PatientUserLoginRequestDTO request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return new UserResponseModel
            {
                AccessTokenExpireDate = null,
                AuthenticateResult = false,
                AuthToken = null
            };
        }

        var patient = await context.Patients
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (patient == null || !BCrypt.Net.BCrypt.Verify(request.Password, patient.PasswordHash))
        {
            return new UserResponseModel
            {
                AccessTokenExpireDate = null,
                AuthenticateResult = false,
                AuthToken = null
            };
        }

        var generatedToken = await tokenService.GenerateToken(new GenerateTokenRequestDTO { UserId = patient.Id, Name = patient.Name, Mail = patient.Email });
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
    public async Task<bool> IsPatientExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Patients
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}
