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
    [HttpGet("all")]
    public async Task<IActionResult> GetAllRatings()
    {
        var getAllRatings = await ratingService.GetAllRatings();
        if (getAllRatings.Count < 1 || getAllRatings == null)
        {
            return NotFound("There is no ratings.");
        }
        return Ok(getAllRatings);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRatingById(Guid id)
    {
        var getRat = await ratingService.GetRatingById(id);
        return Ok(getRat);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRatingById([FromRoute] Guid id, RatingRequestModel request)
    {
        var ratingId = await ratingService.GetRatingById(id);

        if (ratingId == null)
        {
            return NotFound();
        }

        var mapping = request.Map();
        var rating = await ratingService.UpdateRatingById(id, mapping);
        return Ok(rating);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRatingById([FromRoute] Guid id)
    {
        var delRat = await ratingService.DeleteRatingById(id);
        if (!delRat)
        {
            return NotFound();   
        }
        return Ok(delRat);
    }
}
