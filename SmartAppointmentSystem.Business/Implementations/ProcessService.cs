﻿using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class ProcessService(AppointmentContext context) : IProcessService
{
    public async Task<bool> CreateProcess(ProcessRequestDTO processRequestDTO)
    {
        var filled = processRequestDTO.Map();
        var process = await context.Processes.AddAsync(filled);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
    public async Task<Process> GetProcessById(Guid id)
    {
        var getProcess = await context.Processes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return getProcess;
    }
    public async Task<List<Process>> GetAllProcesses()
    {
        var getAll = await context.Processes.AsNoTracking().ToListAsync();
        return getAll;
    }
    public async Task<bool> UpdateProcessById(Guid id, ProcessRequestDTO processRequestDTO)
    {
        var getId = await context.Processes.FirstOrDefaultAsync(x => x.Id == id);
        if (getId == null)
        {
            return false;
        }
        processRequestDTO.Map(getId);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
    public async Task<bool> DeleteProcessById(Guid id)
    {
        var delete = await context.Processes.FirstOrDefaultAsync(x => x.Id == id);
        context.Processes.Remove(delete);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
}
