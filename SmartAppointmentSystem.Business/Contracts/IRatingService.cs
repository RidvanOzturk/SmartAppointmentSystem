using SmartAppointmentSystem.Business.DTOs.RequestDTOs;
using SmartAppointmentSystem.Business.DTOs.ResponseDTOs;
using SmartAppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IRatingService
{
    Task CreateRatingAsync(RatingRequestDTO ratingRequestDTO, CancellationToken cancellationToken = default);
    Task<RatingResponseDTO?> GetRatingByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateRatingByIdAsync(Guid id, RatingRequestDTO ratingRequestDTO, CancellationToken cancellationToken = default);
    Task DeleteRatingByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<RatingResponseDTO?>> GetAllRatingsAsync(CancellationToken cancellationToken = default);
    Task<bool> isRatingExistAsync(Guid id, CancellationToken cancellationToken = default);
}
