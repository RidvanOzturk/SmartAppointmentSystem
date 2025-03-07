using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class DoctorUserService(AppointmentContext context, ITokenService tokenService) : IDoctorUserService
{
    public async Task<Doctor> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Doctors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    public async Task<List<Doctor>> GetAllDoctorsAsync(CancellationToken cancellationToken)
    {
        return await context.Doctors
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    public async Task<List<Doctor>> GetDoctorsWithMostAppointmentsAsync(CancellationToken cancellationToken)
    {
        return await context.Doctors
            .AsNoTracking()
            .OrderByDescending(x=>x.Appointments.Count)
            .ToListAsync(cancellationToken);
    }
    public async Task<List<DoctorsRatingDTO>> GetTopRatedDoctorsAsync(CancellationToken cancellationToken)
    {
        var queryResult = await context.Doctors
            .Include(d => d.Ratings)
            .Select(d => new
        {
            Doctor = d,
            AverageRating = d.Ratings.Any() ? d.Ratings.Average(r => r.Score) : 0
        })
            .OrderByDescending(x => x.AverageRating)
            .ToListAsync(cancellationToken);

        var result = queryResult.Select(x =>
        {
            var dto = new DoctorsRatingDTO();
            dto.Map(x.Doctor); 
            dto.AverageRating = Math.Round(x.AverageRating, 2); 
            return dto;
        }).ToList();
        return result;
    }
    public async Task CreateDoctorAsync(DoctorUserSignUpRequestDTO requestDTO, CancellationToken cancellationToken)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password);
        var doctorEntity = requestDTO.Map();
        doctorEntity.PasswordHash = hashedPassword;
        await context.Doctors
            .AddAsync(doctorEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<List<Doctor>> SearchDoctorsNameAsync(string query, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(query))
        {
            return await context.Doctors
                .ToListAsync(cancellationToken);
        }
        return await context.Doctors
            .AsNoTracking()
            .Where(d => EF.Functions.Like(d.Name, $"%{query}%"))
            .ToListAsync(cancellationToken);
    }

    public async Task<UserResponseModel> LoginUserAsync(DoctorUserLoginRequestDTO request, CancellationToken cancellationToken)
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

        var user = await context.Doctors
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return new UserResponseModel
            {
                AuthenticateResult = false,
                AuthToken = null,
                AccessTokenExpireDate = null,
            };
        }

        var generatedToken = await tokenService.GenerateToken(new GenerateTokenRequestDTO { UserId = user.Id, Name = user.Name, Mail = user.Email});
        return new UserResponseModel
        {
            AccessTokenExpireDate = generatedToken.TokenExpireDate,
            AuthenticateResult = true,
            AuthToken = generatedToken.Token
        };
    }
    public async Task UpdateDoctorByIdAsync(Guid id, DoctorUserRequestDTO requestDTO, CancellationToken cancellationToken)
    {
        var doctor = await context.Doctors
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        requestDTO.Map(doctor);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteDoctorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        context.Doctors
            .Remove(doctor);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<bool> IsDoctorExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Doctors
            .AnyAsync(x=> x.Id == id, cancellationToken);
    } 
}
