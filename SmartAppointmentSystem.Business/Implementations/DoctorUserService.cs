using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.DTOs.RequestDTOs;
using SmartAppointmentSystem.Business.DTOs.ResponseDTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class DoctorUserService(AppointmentContext context, ITokenService tokenService, IConfiguration configuration) : IDoctorUserService
{
    public async Task<DoctorResponseDTO> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var doctor = await context.Doctors
         .AsNoTracking()
         .Include(x => x.Branch)
         .Include(x => x.Ratings)
         .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (doctor is null)
        {
            return null;
        }

        return new DoctorResponseDTO(
             doctor.Id,
             doctor.Name,
             doctor.Email,
             doctor.Description,
             doctor.Image,
             doctor.CreatedAt,
             doctor.Branch != null ? new BranchResponseDTO(doctor.Branch.Id, doctor.Branch.Title, doctor.Branch.Description) : null,
             FunctionExtensions.CalculateAverageRating(doctor.Ratings)
        );
    }
  
    public async Task<TokenReponse?> LoginUserAsync(DoctorUserLoginRequestDTO request, CancellationToken cancellationToken)
    {
        var doctor = await context.Doctors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (doctor == null || !BCrypt.Net.BCrypt.Verify(request.Password, doctor.PasswordHash))
        {
            return null;
        }

        int tokenExpiryMinutes = configuration.GetValue<int>("TokenSettings:ExpiresInMinutes");
        var expireDate = DateTime.UtcNow.AddMinutes(tokenExpiryMinutes);

        var generatedTokenResponse = tokenService.GenerateToken(new TokenRequest
        {
            UserId = doctor.Id,
            Name = doctor.Name,
            Mail = doctor.Email
        });

        var refreshToken = tokenService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            Expiration = DateTime.UtcNow.AddDays(7),
            DoctorId = doctor.Id,
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
    public async Task<List<DoctorResponseDTO>> GetAllDoctorsAsync(CancellationToken cancellationToken)
    {
        var doctors = await context.Doctors
                .AsNoTracking()
                .Include(x => x.Branch)
                .Include(x => x.Ratings)
                .Select(x => new DoctorResponseDTO(
                    x.Id,
                    x.Name,
                    x.Email,
                    x.Description,
                    x.Image,
                    x.CreatedAt,
                    x.Branch != null ? new BranchResponseDTO(x.Branch.Id, x.Branch.Title, x.Branch.Description) : new BranchResponseDTO(0, string.Empty, string.Empty),
                    FunctionExtensions.CalculateAverageRating(x.Ratings)
                ))
                .ToListAsync(cancellationToken);

        return doctors;
    }

    public async Task<List<DoctorResponseDTO>> GetNewAddedDoctors(CancellationToken cancellationToken)
    {
        return await context.Doctors
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new DoctorResponseDTO(
                    x.Id,
                    x.Name,
                    x.Email,
                    x.Description,
                    x.Image,
                    x.CreatedAt,
                    x.Branch != null ? new BranchResponseDTO(x.Branch.Id, x.Branch.Title, x.Branch.Description) : new BranchResponseDTO(0, string.Empty, string.Empty),
                    FunctionExtensions.CalculateAverageRating(x.Ratings)
                ))
            .ToListAsync(cancellationToken);
    }
    public async Task<List<DoctorResponseDTO>> GetDoctorsWithMostAppointmentsAsync(CancellationToken cancellationToken)
    {
        return await context.Doctors
            .AsNoTracking()
            .OrderByDescending(x => x.Appointments.Count)
            .Select(x => new DoctorResponseDTO(
                x.Id,
                x.Name,
                x.Email,
                x.Description,
                x.Image,
                x.CreatedAt,
                x.Branch != null
                    ? new BranchResponseDTO(x.Branch.Id, x.Branch.Title, x.Branch.Description)
                    : null,
                FunctionExtensions.CalculateAverageRating(x.Ratings)
            ))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<DoctorResponseDTO>> GetTopRatedDoctorsAsync(CancellationToken cancellationToken)
    {
        return await context.Doctors
            .AsNoTracking()
            .Select(d => new DoctorResponseDTO(
                d.Id,
                d.Name,
                d.Email,
                d.Description,
                d.Image,
                d.CreatedAt,
                d.Branch != null
                    ? new BranchResponseDTO(d.Branch.Id, d.Branch.Title, d.Branch.Description)
                    : null,
                Math.Round(FunctionExtensions.CalculateAverageRating(d.Ratings), 2)
            ))
            .OrderByDescending(x => x.AverageRating)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> CreateDoctorAsync(DoctorUserSignUpRequestDTO requestDTO, CancellationToken cancellationToken)
    {
        var user = await context.Doctors.FirstOrDefaultAsync(x => x.Email == requestDTO.Email, cancellationToken);
        if (user == null)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password);
            var doctorEntity = requestDTO.Map();
            doctorEntity.PasswordHash = hashedPassword;
            await context.Doctors
                .AddAsync(doctorEntity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;


    }
    public async Task<List<DoctorResponseDTO>> SearchDoctorsNameAsync(string query, CancellationToken cancellationToken)
    {
        IQueryable<Doctor> doctorQuery = context.Doctors.AsNoTracking();

        if (!string.IsNullOrEmpty(query))
            doctorQuery = doctorQuery.Where(d => EF.Functions.Like(d.Name, $"%{query}%"));

        return await doctorQuery
            .Select(d => new DoctorResponseDTO(
                d.Id,
                d.Name,
                d.Email,
                d.Description,
                d.Image,
                d.CreatedAt,
                d.Branch != null
                    ? new BranchResponseDTO(d.Branch.Id, d.Branch.Title, d.Branch.Description)
                    : null,
                Math.Round(FunctionExtensions.CalculateAverageRating(d.Ratings), 2)
            ))
            .ToListAsync(cancellationToken);
    }


    public async Task<List<DoctorResponseDTO>> SearchDoctorsBranchAsync(int query, CancellationToken cancellationToken)
    {
        return await context.Doctors
            .AsNoTracking()
            .Where(x => x.BranchId == query)
            .Select(d => new DoctorResponseDTO(
                d.Id,
                d.Name,
                d.Email,
                d.Description,
                d.Image,
                d.CreatedAt,
                d.Branch != null
                    ? new BranchResponseDTO(d.Branch.Id, d.Branch.Title, d.Branch.Description)
                    : null,
                Math.Round(FunctionExtensions.CalculateAverageRating(d.Ratings), 2)
            ))
            .ToListAsync(cancellationToken);
    }



    public async Task UpdateDoctorByIdAsync(Guid id, DoctorUserRequestDTO requestDTO, CancellationToken cancellationToken)
    {
        var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (doctor.Name != requestDTO.Name) 
        {
            doctor.Name = requestDTO.Name;
        }

        if (doctor.Email != requestDTO.Email)
        {
            doctor.Email = requestDTO.Email;
        }

        if (doctor.Description != requestDTO.Description)
        {
            doctor.Description = requestDTO.Description;
        }

        if (doctor.Image != requestDTO.Image)
        {
            doctor.Image = requestDTO.Image;
        }

        if (!string.IsNullOrWhiteSpace(requestDTO.Password))
        {
            if (!BCrypt.Net.BCrypt.Verify(requestDTO.Password, doctor.PasswordHash))
            {
                doctor.PasswordHash = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password);
            }
        }

        if (doctor.BranchId != requestDTO.BranchId)
        {
            doctor.BranchId = requestDTO.BranchId;
        }

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
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}
