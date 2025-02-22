﻿using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IPatientUserService
{
    Task<bool> RegisterAsync(PatientUserRequestDTO requestDTO);
    public Task<UserResponseModel> LoginUserAsync(PatientUserRequestDTO request);
    Task<List<Patient>> GetUsersAsync();
    Task<Patient> GetUserByIdAsync(Guid id);
    Task<bool> DeleteUserByIdAsync(Guid id);
}
