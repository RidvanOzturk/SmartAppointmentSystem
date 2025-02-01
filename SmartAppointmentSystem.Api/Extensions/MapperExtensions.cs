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
            DateTime = value.DateTime,
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
            DoctorId = value.DoctorId
        };
    }
    public static DoctorUserRequestDTO Map(this DoctorUserRequestModel value)
    {
        return new DoctorUserRequestDTO
        {
            BranchId = value.BranchId,
            Description = value.Description,
            Email = value.Email,
            Name = value.Name,
            PasswordHash = value.PasswordHash,
        };
    }
}
