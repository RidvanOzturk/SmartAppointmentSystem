﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class DoctorUserService(AppointmentContext context, ITokenService tokenService, IMapper mapper) : IDoctorUserService
{
    public async Task<DoctorResponseDTO> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var doctor = await context.Doctors
         .AsNoTracking()
         .Include(x => x.Branch)
         .Include(x => x.Ratings)
         .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (doctor == null)
            return null;
        return new DoctorResponseDTO(
             doctor.Id,
             doctor.Name,
             doctor.Email,
             doctor.Description,
             doctor.Image,
             doctor.BranchId,
             doctor.CreatedAt,
             doctor.Branch != null ? new BranchResponseDTO(doctor.Branch.Id, doctor.Branch.Title) : null,
             FunctionExtensions.CalculateAverageRating(doctor.Ratings)
        );
    }
    public async Task<List<AllDoctorResponseDTO>> GetAllDoctorsAsync(CancellationToken cancellationToken)
    {
        var doctors = await context.Doctors
                .AsNoTracking()
                .Include(x => x.Branch)   
                .Include(x => x.Ratings)   
                .Select(x => new AllDoctorResponseDTO(
                    x.Id,
                    x.Name,
                    x.Email,
                    x.BranchId,
                    x.Branch != null ? new BranchResponseDTO(x.Branch.Id, x.Branch.Title) : null,
                    FunctionExtensions.CalculateAverageRating(x.Ratings)
                ))
                .ToListAsync(cancellationToken);

        return doctors;
    }

    public async Task<List<Doctor>> GetNewAddedDoctors(CancellationToken cancellationToken)
    {
        return await context.Doctors
            .AsNoTracking()
            .OrderByDescending(x=> x.CreatedAt)
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
            AverageRating = FunctionExtensions.CalculateAverageRating(d.Ratings)
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
    public async Task<List<Doctor>> SearchDoctorsNameAsync(string query, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(query))
        {
            return await context.Doctors
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        return await context.Doctors
            .AsNoTracking()
            .Where(d => EF.Functions.Like(d.Name, $"%{query}%"))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Doctor>> SearchDoctorsBranchAsync(int query, CancellationToken cancellationToken)
    {
      
        return await context.Doctors.AsNoTracking()
             .Where(x=> x.BranchId == query)
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
                AuthToken = null,
                RefreshToken = null

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

        var generatedToken = await tokenService.GenerateToken(new GenerateTokenRequestDTO 
        { 
            UserId = user.Id,
            Name = user.Name,
            Mail = user.Email
        });
        var refreshTokenString = tokenService.GenerateRefreshTokenAsync();
        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshTokenString.Result,
            Expiration = DateTime.UtcNow.AddDays(7),
            DoctorId = user.Id, 
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow
        };
        await context.RefreshTokens.AddAsync(refreshTokenEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new UserResponseModel
        {
            AccessTokenExpireDate = generatedToken.TokenExpireDate,
            AuthenticateResult = true,
            AuthToken = generatedToken.Token,
            RefreshToken = refreshTokenString.Result
        };
    }
    public async Task UpdateDoctorByIdAsync(Guid id, DoctorUserRequestDTO requestDTO, CancellationToken cancellationToken)
    {
        var doctor = await context.Doctors
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        mapper.Map(requestDTO, doctor);
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
    public async Task<UserResponseModel> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var storedToken = await context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == refreshToken, cancellationToken);

        if (storedToken == null || storedToken.Expiration < DateTime.UtcNow || storedToken.IsRevoked)
        {
            throw new Exception("Invalid or expired refresh token.");
        }
        var doctor = await context.Doctors
            .FirstOrDefaultAsync(d => d.Id == storedToken.DoctorId, cancellationToken);
        if (doctor == null)
        {
            throw new Exception("User not found.");
        }
        var generatedToken = await tokenService.GenerateToken(new GenerateTokenRequestDTO
        {
            UserId = doctor.Id,
            Name = doctor.Name,
            Mail = doctor.Email
        });
        var newRefreshToken = tokenService.GenerateRefreshTokenAsync();
        storedToken.Token = newRefreshToken.Result;
        storedToken.Expiration = DateTime.UtcNow.AddDays(7);
        storedToken.CreatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync(cancellationToken);
        return new UserResponseModel
        {
            AuthenticateResult = true,
            AuthToken = generatedToken.Token,
            AccessTokenExpireDate = generatedToken.TokenExpireDate,
            RefreshToken = newRefreshToken.Result
        };
    }

}
