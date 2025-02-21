using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController(IBranchService branchService) : ControllerBase
    {

        [HttpGet("search")]
        public async Task<IActionResult> GetBranchBySearchAsync([FromQuery] string query)
        {
            var branches = await branchService.GetBranchesSearch(query);
            if (branches == null || branches.Count <1)
            {
                return NotFound("There is no branches like " + $"{query}");
            }
            return Ok(branches);
        }
    }
}
