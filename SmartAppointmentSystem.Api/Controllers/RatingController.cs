using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.Implementations;

namespace SmartAppointmentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RatingController(IRatingService ratingService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateRating(RatingRequestModel requestModel)
    {
        var mapping = requestModel.Map();
        var rating = await ratingService.CreateRating(mapping);
        return Ok(rating);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRatingById(Guid id)
    {
        var getRat = await ratingService.GetRatingById(id);
        return Ok(getRat);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRating([FromRoute] Guid id, RatingRequestModel request)
    {
        var ratingId = await ratingService.GetRatingById(id);

        if (ratingId == null)
        {
            return NotFound();
        }

        var mapping = request.Map();
        var rating = await ratingService.UpdateRating(id, mapping);
        return Ok(rating);
    }
}
