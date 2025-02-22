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
    Task<bool> CreateRatingAsync(RatingRequestDTO ratingRequestDTO);
    Task<Rating> GetRatingByIdAsync(Guid id);
    Task<bool> UpdateRatingByIdAsync(Guid id, RatingRequestDTO ratingRequestDTO);
    Task<bool> DeleteRatingByIdAsync(Guid id);
    Task<List<Rating>> GetAllRatingsAsync();
}
