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
    public async Task<Doctor> GetDoctorByIdAsync(Guid id)
    {
        var getDoctor = await context.Doctors.
            AsNoTracking().
            FirstOrDefaultAsync(x => x.Id == id);
        return getDoctor;
    }
    public async Task<List<Doctor>> GetAllDoctorsAsync()
    {
        var getAllDoc = await context.Doctors.
            AsNoTracking().
            ToListAsync();
        return getAllDoc;
    }
    public async Task<List<Doctor>> GetDoctorWithMostAppointmentsAsync()
    {
        var getDoctor = await context.Doctors.
            AsNoTracking().
            OrderByDescending(x=>x.Appointments.Count).
            ToListAsync();
        return getDoctor;
    }
    public async Task<List<DoctorsRatingDTO>> GetTopRatedDoctorsAsync()
    {
        var queryResult = await context.Doctors
        .Include(d => d.Ratings)
        .Select(d => new
        {
            Doctor = d,
            AverageRating = d.Ratings.Any() ? d.Ratings.Average(r => r.Score) : 0
        })
        .OrderByDescending(x => x.AverageRating)
        .ToListAsync();

        var result = queryResult.Select(x =>
        {
            var dto = new DoctorsRatingDTO();
            dto.Map(x.Doctor); 
            dto.AverageRating = Math.Round(x.AverageRating, 2); 
            return dto;
        }).ToList();
        return result;
    }
    public async Task<bool> CreateDoctorAsync(DoctorUserRequestDTO requestDTO)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password);
        var doctorEntity = requestDTO.Map();
        doctorEntity.PasswordHash = hashedPassword;
        var createDoc = await context.Doctors.AddAsync(doctorEntity);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
    public async Task<List<Doctor>> SearchDoctorsNameAsync(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return await context.Doctors.ToListAsync();
        }
        var searchDoctorsName = await context.Doctors
            .AsNoTracking()
            .Where(d => EF.Functions.Like(d.Name, $"%{query}%"))
            .ToListAsync();
        return searchDoctorsName;
    }

    public async Task<UserResponseModel> LoginUserAsync(DoctorUserLoginRequestDTO request)
    {
        if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Password))
        {
            throw new ArgumentNullException(nameof(request));
        }

        var user = await context.Doctors.FirstOrDefaultAsync(x => x.Name == request.Name);
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
    public async Task<bool> UpdateDoctorByIdAsync(Guid id, DoctorUserRequestDTO requestDTO)
    {
        var doctorId = await context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        requestDTO.Map(doctorId);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
    public async Task<bool> DeleteDoctorByIdAsync(Guid id)
    {
        var doctorId = await context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        context.Doctors.Remove(doctorId);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
}
