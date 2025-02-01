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
    public async Task<List<Rating>> GetAllRatings()
    {
        var getAll = await appointmentContext.Ratings.AsNoTracking().ToListAsync();
        return getAll;
    }
    public async Task<Rating> GetRatingById(Guid id)
    {
        var rating = await appointmentContext.Ratings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return rating;
    }
    public async Task<bool> UpdateRatingById(Guid id, RatingRequestDTO ratingRequestDTO)
    {
        var ratingId = await appointmentContext.Ratings.FirstOrDefaultAsync(x => x.Id == id);
        ratingRequestDTO.Map(ratingId);
        var changes = await appointmentContext.SaveChangesAsync();
        return changes > 0;
    }
    public async Task<bool> DeleteRatingById(Guid id)
    {
        var ratingId = await appointmentContext.Ratings.FirstOrDefaultAsync(x => x.Id == id);
        appointmentContext.Remove(ratingId);
        var changes = await appointmentContext.SaveChangesAsync();
        return changes > 0;
    }
}
