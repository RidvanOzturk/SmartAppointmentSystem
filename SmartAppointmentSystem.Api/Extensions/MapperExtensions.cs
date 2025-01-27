using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Api.Extensions;

public static class MapperExtensions
{
    public static UserRequestDTO Map(this UserRequestModel value)
    {
        return new UserRequestDTO
        {
            Name = value.Name,
            Email = value.Email,
            Password = value.Password,
            Role = value.Role,
        };
    }
    public static AppointmentRequestDTO Map(this AppointmentRequestModel value)
    {
        return new AppointmentRequestDTO
        {
            ProfessionalId = value.ProfessionalId,
            CustomerId = value.CustomerId,
            DateTime = value.DateTime,
            Notes = value.Notes,
            Status = value.Status,
        };
    }
    public static RatingRequestDTO Map(this RatingRequestModel value)
    {
        return new RatingRequestDTO
        {
            ProfessionalId = value.ProfessionalId,
            CustomerId = value.CustomerId,
            Comment = value.Comment,
            CreatedAt = value.CreatedAt,
            Score = value.Score,
        };
    }
    public static TimeSlotRequestDTO Map(this TimeSlotRequestModel value) 
    {
        return new TimeSlotRequestDTO
        {
            ProfessionalId = value.ProfessionalId,
            ProcessId = value.ProcessId,
            AvailableFrom = value.AvailableFrom,
            AvailableTo = value.AvailableTo,
        };
    }
    public static ProcessRequestDTO Map(this ProcessRequestModel value)
    {
        return new ProcessRequestDTO
        {
            Name = value.Name,
            Duration = value.Duration,
            ProfessionalId = value.ProfessionalId
        };
    }
}
