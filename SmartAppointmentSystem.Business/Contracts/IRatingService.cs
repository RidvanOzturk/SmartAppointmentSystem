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
    Task<bool> CreateRating(RatingRequestDTO ratingRequestDTO);
    Task<Rating> GetRatingById(Guid id);
    Task<bool> UpdateRatingById(Guid id, RatingRequestDTO ratingRequestDTO);
    Task<bool> DeleteRatingById(Guid id);
    Task<List<Rating>> GetAllRatings();
}
