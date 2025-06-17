using SmartAppointmentSystem.Business.DTOs.ResponseDTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IBranchService
{
    Task<List<BranchResponseDTO>> GetBranchesSearchAsync(string query, CancellationToken cancellationToken = default);
    Task<List<BranchResponseDTO>> GetAllBranchesAsync(CancellationToken cancellationToken = default);
}
