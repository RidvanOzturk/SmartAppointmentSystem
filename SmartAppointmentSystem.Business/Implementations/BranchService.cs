using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs.ResponseDTOs;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class BranchService(AppointmentContext context) : IBranchService
{
    public async Task<List<BranchResponseDTO>> GetBranchesSearchAsync(string query, CancellationToken cancellationToken)
    {
        IQueryable<Branch> branchQuery = context.Branches.AsNoTracking();

        if (!string.IsNullOrEmpty(query))
            branchQuery = branchQuery.Where(d => EF.Functions.Like(d.Title, $"%{query}%"));

        return await branchQuery
            .Select(x => new BranchResponseDTO(x.Id, x.Title, x.Description))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<BranchResponseDTO>> GetAllBranchesAsync(CancellationToken cancellationToken)
    {
        return await context.Branches.AsNoTracking()
            .Select(x=> new BranchResponseDTO(x.Id, x.Title, x.Description))
            .ToListAsync(cancellationToken);
    }
}
