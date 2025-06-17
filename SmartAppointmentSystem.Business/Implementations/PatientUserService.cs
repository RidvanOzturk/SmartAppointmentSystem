using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.DTOs.RequestDTOs;
using SmartAppointmentSystem.Business.DTOs.ResponseDTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;
using System.Linq;
namespace SmartAppointmentSystem.Business.Implementations;

public class PatientUserService(AppointmentContext context, ITokenService tokenService, IConfiguration configuration) : IPatientUserService
{
    public async Task<bool> CreatePatientAsync(PatientUserRequestDTO requestDTO, CancellationToken cancellationToken = default)
    {
        var user = await context.Patients.FirstOrDefaultAsync(x=>x.Email == requestDTO.Email);
        if (user == null)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password);
            var patient = requestDTO.Map();
            patient.PasswordHash = hashedPassword;
            await context.Patients
                .AddAsync(patient, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }

    public async Task<TokenReponse?> LoginPatientUserAsync(PatientUserLoginRequestDTO request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return null;
        }

        var patient = await context.Patients.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (patient == null || !BCrypt.Net.BCrypt.Verify(request.Password, patient.PasswordHash))
        {
            return null;
        }

        int tokenExpiryMinutes = configuration.GetValue<int>("TokenSettings:ExpiresInMinutes");
        var expireDate = DateTime.UtcNow.AddMinutes(tokenExpiryMinutes);

        var generatedTokenResponse = tokenService.GenerateToken(new TokenRequest
        {
            UserId = patient.Id,
            Name = patient.Name,
            Mail = patient.Email
        });

        var refreshToken = tokenService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            Expiration = DateTime.UtcNow.AddDays(7), 
            PatientId = patient.Id,
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow
        };
        context.RefreshTokens.Add(refreshTokenEntity);
        await context.SaveChangesAsync(cancellationToken);

        return new TokenReponse
        {
            AccessToken = generatedTokenResponse,
            RefreshToken = refreshToken,
            ExpireDate = expireDate
        };
    }
    public async Task<List<PatientResponseDTO>> GetPatientUsersAsync(CancellationToken cancellationToken = default)
    {
        return await context.Patients
            .AsNoTracking()
            .Include(x => x.Ratings)
            .Include(x => x.Appointments)
            .Select(x => new PatientResponseDTO(
                x.Name,
                x.Email,
                x.Appointments,
                x.Ratings
                ))
            .ToListAsync(cancellationToken);
    }
    public async Task<PatientResponseDTO?> GetPatientUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var patient = await context.Patients
            .AsNoTracking()
            .Include(x => x.Ratings)
            .Include(x => x.Appointments)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (patient == null)
        {
            return null;
        }
        return new PatientResponseDTO(
            patient.Name,
            patient.Email,
            patient.Appointments,
            patient.Ratings
        );
    }

    public async Task UpdatePatientById(Guid id, PatientUserRequestDTO requestDTO, CancellationToken cancellationToken = default)
    {
        var patient = await context.Patients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (patient.Name != requestDTO.Name)
        {
            patient.Name = requestDTO.Name;
        }

        if (patient.Email != requestDTO.Email)
        {
            patient.Email = requestDTO.Email;
        }  

        if (!string.IsNullOrWhiteSpace(requestDTO.Password))
        {
            if (!BCrypt.Net.BCrypt.Verify(requestDTO.Password, patient.PasswordHash))
            {
                patient.PasswordHash = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password);
            }
        }
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePatientUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var patient = await context.Patients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        context.Patients.Remove(patient);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<bool> IsPatientExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Patients
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}
