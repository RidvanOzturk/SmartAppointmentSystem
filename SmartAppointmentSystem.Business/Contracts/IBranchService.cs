using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IBranchService
{
    Task<List<Branch>> GetBranchesSearchAsync(string query, CancellationToken cancellationToken = default);
    Task<List<Branch>> GetAllBranchesAsync(CancellationToken cancellationToken = default);
}
