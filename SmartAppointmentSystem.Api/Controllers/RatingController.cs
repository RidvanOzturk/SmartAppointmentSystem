using Microsoft.AspNetCore.Http;
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
}
