using Microsoft.AspNetCore.Mvc;
using SmartAppointmentSystem.Api.Extensions;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.Contracts;

namespace SmartAppointmentSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RatingController(IRatingService ratingService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateRating([FromBody] RatingRequestModel requestModel, CancellationToken cancellationToken)
    {
        var rating = requestModel.Map();
        await ratingService.CreateRatingAsync(rating, cancellationToken);
        return Ok();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllRatings(CancellationToken cancellationToken)
    {
        var ratings = await ratingService.GetAllRatingsAsync(cancellationToken);
        if (ratings.Count == 0)
        {
            return NotFound();
        }
        return Ok(ratings);
    }
 
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRating(Guid id, CancellationToken cancellationToken)
    {
        var rating = await ratingService.GetRatingByIdAsync(id, cancellationToken);
        return Ok(rating);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRating([FromRoute] Guid id, RatingRequestModel request, CancellationToken cancellationToken)
    {
        var isRatingExist = await ratingService.isRatingExistAsync(id, cancellationToken);
        if (!isRatingExist)
        {
            return NotFound();
        }
        var rating = request.Map();
        await ratingService.UpdateRatingByIdAsync(id, rating, cancellationToken);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRating([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var isRatingExist = await ratingService.isRatingExistAsync(id, cancellationToken);
        if (!isRatingExist)
        {
            return NotFound();
        }
        await ratingService.DeleteRatingByIdAsync(id, cancellationToken);
        return Ok();
    }
}
