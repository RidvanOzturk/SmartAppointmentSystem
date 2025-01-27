using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IProcessService
{
    Task<bool> CreateProcess(ProcessRequestDTO processRequestDTO);
    Task<Process> GetProcessById(Guid id);
    Task<List<Process>> GetAllProcesses();
    Task<bool> UpdateProcessById(Guid id, ProcessRequestDTO processRequestDTO);
    Task<bool> DeleteProcessById(Guid id);
}
