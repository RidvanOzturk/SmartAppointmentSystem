using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IBranchService
{
    Task<List<Branch>> GetBranchesSearchAsync(string query, CancellationToken cancellationToken = default);
    Task<List<BranchRequestDTO>> GetAllBranchesAsync(CancellationToken cancellationToken = default);
}
