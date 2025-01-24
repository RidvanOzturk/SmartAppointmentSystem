using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Business.Extensions;

public static class MapperExtensions
{
    public static User Map(this UserRequestDTO registerRequest)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = registerRequest.Name,
            Email = registerRequest.Email,
            PasswordHash = registerRequest.Password,
            Role = registerRequest.Role,
        };
    }
    public static Appointment Map(this AppointmentRequestDTO createRequest)
    {
        return new Appointment
        {
            Id = Guid.NewGuid(),
            DateTime = createRequest.DateTime,
            CustomerId = createRequest.CustomerId,
            Notes = createRequest.Notes,
            Status = createRequest.Status,
            ProfessionalId = createRequest.ProfessionalId,
        };
    }
    public static Rating Map(this RatingRequestDTO ratingRequest)
    {
        return new Rating
        {
            Id = Guid.NewGuid(),
            ProfessionalId = ratingRequest.ProfessionalId,
            CustomerId = ratingRequest.CustomerId,
            Comment = ratingRequest.Comment,
            CreatedAt = ratingRequest.CreatedAt,
            Score = ratingRequest.Score,
        };
    }
    public static void Map(this RatingRequestDTO source, Rating target)
    {
        target.ProfessionalId = source.ProfessionalId;
        target.CustomerId = source.CustomerId;
        target.Comment = source.Comment;
        target.Score = source.Score;
        target.CreatedAt = source.CreatedAt;
    }
}
