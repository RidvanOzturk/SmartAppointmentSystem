using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class RatingService(AppointmentContext context) : IRatingService
{
    public async Task<bool> CreateRatingAsync(RatingRequestDTO ratingRequestDTO)
    {
        var ratingEntity = ratingRequestDTO.Map();
        var rating = context.Ratings.AddAsync(ratingEntity);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
    public async Task<List<Rating>> GetAllRatingsAsync()
    {
        var getAll = await context.Ratings
            .AsNoTracking()
            .ToListAsync();
        return getAll;
    }
    public async Task<Rating> GetRatingByIdAsync(Guid id)
    {
        var rating = await context.Ratings
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        if (rating == null) 
        { 
            return null;
        }
        return rating;
    }
    public async Task<bool> UpdateRatingByIdAsync(Guid id, RatingRequestDTO ratingRequestDTO)
    {
        var ratingId = await context.Ratings.FirstOrDefaultAsync(x => x.Id == id);
        ratingRequestDTO.Map(ratingId);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
    public async Task<bool> DeleteRatingByIdAsync(Guid id)
    {
        var ratingId = await context.Ratings.FirstOrDefaultAsync(x => x.Id == id);
        context.Remove(ratingId);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
}
