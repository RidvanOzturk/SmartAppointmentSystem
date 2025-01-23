using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class RatingService(AppointmentContext appointmentContext) : IRatingService
{
    public async Task<bool> CreateRating(RatingRequestDTO ratingRequestDTO)
    {
        var filled = ratingRequestDTO.Map();
        var rating = appointmentContext.Ratings.AddAsync(filled);
        var changes = await appointmentContext.SaveChangesAsync();
        return changes > 0;
    }
    public async Task<Rating> GetRatingById(Guid id)
    {
        var rating = await appointmentContext.Ratings.FirstOrDefaultAsync(x => x.Id == id);
        return rating;
    }
}
