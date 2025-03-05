using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IRatingService
{
    Task CreateRatingAsync(RatingRequestDTO ratingRequestDTO, CancellationToken cancellationToken);
    Task<Rating> GetRatingByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateRatingByIdAsync(Guid id, RatingRequestDTO ratingRequestDTO, CancellationToken cancellationToken);
    Task DeleteRatingByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Rating>> GetAllRatingsAsync(CancellationToken cancellationToken);
}
