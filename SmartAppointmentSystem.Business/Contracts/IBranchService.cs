using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IBranchService
{
    Task<List<Branch>> GetBranchesSearchAsync(string query);
}
