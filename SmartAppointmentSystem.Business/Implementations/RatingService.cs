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
    public async Task CreateRatingAsync(RatingRequestDTO ratingRequestDTO, CancellationToken cancellationToken)
    {
        var rating = ratingRequestDTO.Map();
        await context.Ratings.AddAsync(rating, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<List<Rating>> GetAllRatingsAsync(CancellationToken cancellationToken)
    {
        var ratings = await context.Ratings
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return ratings;
    }
    public async Task<Rating> GetRatingByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var rating = await context.Ratings
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return rating;
    }
    public async Task UpdateRatingByIdAsync(Guid id, RatingRequestDTO ratingRequestDTO, CancellationToken cancellationToken)
    {
        var rating = await context.Ratings.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        ratingRequestDTO.Map(rating);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteRatingByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var rating = await context.Ratings.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        context.Remove(rating);
        await context.SaveChangesAsync(cancellationToken);
    }
}
