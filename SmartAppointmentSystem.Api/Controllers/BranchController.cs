using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController(IBranchService branchService) : ControllerBase
    {

        [HttpGet("search")]
        public async Task<IActionResult> GetBranchBySearch([FromQuery] string query, CancellationToken cancellationToken = default)
        {
            var branches = await branchService.GetBranchesSearchAsync(query, cancellationToken);
            if (branches.Count == 0)
            {
                return NotFound();
            }
            return Ok(branches);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBranches(CancellationToken cancellationToken = default)
        {
            var branches = await branchService.GetAllBranchesAsync(cancellationToken);
            return Ok(branches);
        }
    }
}
