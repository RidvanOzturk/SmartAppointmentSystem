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
        var doctor = await context.Doctors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return doctor;
    }
    public async Task<List<Doctor>> GetAllDoctorsAsync(CancellationToken cancellationToken)
    {
        var doctors = await context.Doctors
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return doctors;
    }
    public async Task<List<Doctor>> GetDoctorsWithMostAppointmentsAsync(CancellationToken cancellationToken)
    {
        var doctors = await context.Doctors
            .AsNoTracking()
            .OrderByDescending(x=>x.Appointments.Count)
            .ToListAsync(cancellationToken);
        return doctors;
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
    public async Task CreateDoctorAsync(DoctorUserRequestDTO requestDTO, CancellationToken cancellationToken)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password);
        var doctorEntity = requestDTO.Map();
        doctorEntity.PasswordHash = hashedPassword;
        await context.Doctors.AddAsync(doctorEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<List<Doctor>> SearchDoctorsNameAsync(string query, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(query))
        {
            return await context.Doctors.ToListAsync(cancellationToken);
        }
        var doctorsName = await context.Doctors
            .AsNoTracking()
            .Where(d => EF.Functions.Like(d.Name, $"%{query}%"))
            .ToListAsync(cancellationToken);
        return doctorsName;
    }

    public async Task<UserResponseModel> LoginUserAsync(DoctorUserLoginRequestDTO request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Password))
        {
            throw new ArgumentNullException(nameof(request));
        }

        var user = await context.Doctors.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Kullanıcı adı veya şifre yanlış.");
        }

        var generatedToken = await tokenService.GenerateToken(new GenerateTokenRequestDTO { UserId = user.Id, Name = user.Name, Mail = user.Email, Role = "Doctor" });
        return new UserResponseModel
        {
            AccessTokenExpireDate = generatedToken.TokenExpireDate,
            AuthenticateResult = true,
            AuthToken = generatedToken.Token
        };
    }
    public async Task<bool> UpdateDoctorByIdAsync(Guid id, DoctorUserRequestDTO requestDTO, CancellationToken cancellationToken)
    {
        var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        requestDTO.Map(doctor);
        var changes = await context.SaveChangesAsync(cancellationToken);
        return changes > 0;
    }
    public async Task<bool> DeleteDoctorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        context.Doctors.Remove(doctor);
        var changes = await context.SaveChangesAsync(cancellationToken);
        return changes > 0;
    }
}
