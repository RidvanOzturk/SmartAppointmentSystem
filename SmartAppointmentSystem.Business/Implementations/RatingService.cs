using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs.RequestDTOs;
using SmartAppointmentSystem.Business.DTOs.ResponseDTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class RatingService(AppointmentContext context) : IRatingService
{
    public async Task CreateRatingAsync(RatingRequestDTO ratingRequestDTO, CancellationToken cancellationToken)
    {
        var rating = ratingRequestDTO.Map();
        await context.Ratings
            .AddAsync(rating, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<List<RatingResponseDTO>> GetAllRatingsAsync(CancellationToken cancellationToken)
    {
        return await context.Ratings
            .AsNoTracking()
            .Select(x=> new RatingResponseDTO(
                x.Id,
                x.DoctorId,
                x.PatientId,
                x.Score,
                x.Comment
                ))
            .ToListAsync(cancellationToken);
    }
    public async Task<RatingResponseDTO?> GetRatingByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Ratings
            .AsNoTracking()
            .Where(x=>x.Id == id)
            .Select(x=> new RatingResponseDTO(
                x.Id,
                x.DoctorId,
                x.PatientId,
                x.Score,
                x.Comment
                ))
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task UpdateRatingByIdAsync(Guid id, RatingRequestDTO ratingRequestDTO, CancellationToken cancellationToken)
    {
        var rating = await context.Ratings
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        ratingRequestDTO.Map(rating);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteRatingByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var rating = await context.Ratings
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        context.Remove(rating);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<bool> isRatingExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Ratings
            .AnyAsync(x=> x.Id == id, cancellationToken);
    }
}
