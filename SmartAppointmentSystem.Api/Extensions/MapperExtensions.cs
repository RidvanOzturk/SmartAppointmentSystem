using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Api.Extensions;

public static class MapperExtensions
{
    public static PatientUserRequestDTO Map(this PatientUserRequestModel value)
    {
        return new PatientUserRequestDTO
        {
            Name = value.Name,
            Email = value.Email,
            Password = value.Password,
        };
    }
    public static AppointmentRequestDTO Map(this AppointmentRequestModel value)
    {
        return new AppointmentRequestDTO
        {
            DoctorId = value.DoctorId,
            PatientId = value.PatientId,
            Time = value.Time,
            Notes = value.Notes,
            Status = value.Status,
        };
    }
    public static RatingRequestDTO Map(this RatingRequestModel value)
    {
        return new RatingRequestDTO
        {
            DoctorId = value.DoctorId,
            PatientId = value.PatientId,
            Comment = value.Comment,
            CreatedAt = value.CreatedAt,
            Score = value.Score,
        };
    }
    public static TimeSlotRequestDTO Map(this TimeSlotRequestModel value)
    {
        return new TimeSlotRequestDTO
        {
            DoctorId = value.DoctorId,
            AvailableDay = value.AvailableDay,
            AppointmentFrequency = value.AppointmentFrequency,
            AvailableFrom = value.AvailableFrom,
            AvailableTo = value.AvailableTo,
        };
    }

    public static DoctorUserRequestDTO Map(this DoctorUserRequestModel value)
    {
        return new DoctorUserRequestDTO
        {
            Email = value.Email,
            Name = value.Name,
            Password = value.Password,
            BranchId = value.BranchId,
            Description = value.Description,
        };
    }
    public static DoctorUserLoginRequestDTO Map(this DoctorUserLoginRequestModel value)
    {
        return new DoctorUserLoginRequestDTO
        {
            Email = value.Email,
            Name = value.Name,
            Password = value.Password,
        };
    }
}
