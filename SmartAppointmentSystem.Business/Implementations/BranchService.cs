using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class BranchService(AppointmentContext context) : IBranchService
{
    public async Task<List<Branch>> GetBranchesSearchAsync(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return await context.Branches.ToListAsync();
        }
        var branches = await context.Branches
            .AsNoTracking()
            .Where(d => EF.Functions.Like(d.Title, $"%{query}%"))
            .ToListAsync();
        return branches;
    }
    public async Task<List<Branch>> GetAllBranchesAsync()
    {
        return await context.Branches.AsNoTracking().ToListAsync();
    }
}
