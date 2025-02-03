﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class DoctorUserService(AppointmentContext context, IConfiguration configuration, ITokenService tokenService) : IDoctorUserService
{
    public async Task<Doctor> GetDoctorById(Guid id)
    {
        var getDoc = await context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        return getDoc;
    }
    public async Task<bool> CreateDoctor(DoctorUserRequestDTO requestDTO)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password);
        var filled = requestDTO.Map();
        filled.PasswordHash = hashedPassword;
        var createDoc = await context.Doctors.AddAsync(filled);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
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
    public async Task<bool> UpdateDoctorById(Guid id, DoctorUserRequestDTO requestDTO)
    {
        var docId = await context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        requestDTO.Map(docId);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
}
