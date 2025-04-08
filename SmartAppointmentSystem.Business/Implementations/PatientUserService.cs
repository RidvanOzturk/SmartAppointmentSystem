using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;
namespace SmartAppointmentSystem.Business.Implementations;

public class PatientUserService(AppointmentContext context, ITokenService tokenService) : IPatientUserService
{
    public async Task<bool> CreatePatientAsync(PatientUserRequestDTO requestDTO, CancellationToken cancellationToken)
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

    public async Task<UserResponseModel> LoginPatientUserAsync(PatientUserLoginRequestDTO request, CancellationToken cancellationToken)
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

        var generatedToken = await tokenService.GenerateToken(new TokenRequest { UserId = patient.Id, Name = patient.Name, Mail = patient.Email });
        return new UserResponseModel
        {
            AccessTokenExpireDate = generatedToken.TokenExpireDate,
            AuthenticateResult = true,
            AuthToken = generatedToken.Token
        };
    }
    public async Task<List<Patient>> GetPatientUsersAsync(CancellationToken cancellationToken)
    {
        //return await context.Patients
        //    .AsNoTracking()
        //    .Include(x=>x.Ratings)
        //    .Include(x=>x.Appointments)
        //    .Select(x=> new PatientResponseDTO(
        //        x.Id,
        //        x.Name,
        //        x.Email,
        //        x.Appointments,
        //        x.Ratings
        //        ))
        //    .ToListAsync(cancellationToken);
        return await context.Patients.ToListAsync();
    }
    public async Task<PatientResponseDTO?> GetPatientUserByIdAsync(Guid id, CancellationToken cancellationToken)
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
            patient.Id,
            patient.Name,
            patient.Email,
            patient.Appointments,
            patient.Ratings
        );
    }
    public async Task DeletePatientUserByIdAsync(Guid id, CancellationToken cancellationToken)
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
