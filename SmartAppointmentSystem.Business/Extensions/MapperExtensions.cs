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
    public static Patient Map(this PatientUserRequestDTO registerRequest)
    {
        return new Patient
        {
            Id = Guid.NewGuid(),
            Name = registerRequest.Name,
            Email = registerRequest.Email,
            PasswordHash = registerRequest.Password,
        };
    }
    public static Appointment Map(this AppointmentRequestDTO createRequest)
    {
        return new Appointment
        {
            Id = Guid.NewGuid(),
            PatientId = createRequest.PatientId,
            Notes = createRequest.Notes,
            Status = createRequest.Status,
            DoctorId = createRequest.DoctorId,
        };
    }
    public static void Map(this AppointmentRequestDTO createRequest, Appointment target)
    {
        target.PatientId = createRequest.PatientId;
        target.Notes = createRequest.Notes;
        target.Status = createRequest.Status;
        target.DoctorId = createRequest.DoctorId;
    }
    public static Rating Map(this RatingRequestDTO ratingRequest)
    {
        return new Rating
        {
            Id = Guid.NewGuid(),
            DoctorId = ratingRequest.DoctorId,
            PatientId = ratingRequest.PatientId,
            Comment = ratingRequest.Comment,
            CreatedAt = ratingRequest.CreatedAt,
            Score = ratingRequest.Score,
        };
    }
    public static Doctor Map(this DoctorUserRequestDTO doctorUserRequest)
    {
        return new Doctor
        {
            Id = Guid.NewGuid(),
            PasswordHash = doctorUserRequest.Password,
            Name = doctorUserRequest.Name,
            Email = doctorUserRequest.Email,
            Description = doctorUserRequest.Description,
            BranchId = doctorUserRequest.BranchId,
        };
    }
    public static void Map(this DoctorUserRequestDTO source, Doctor target)
    {
        target.PasswordHash = source.Password;
        target.Name = source.Name;
        target.Email = source.Email;
        target.Description = source.Email;
        target.BranchId = source.BranchId;
    }
    public static void Map(this RatingRequestDTO source, Rating target)
    {
        target.DoctorId = source.DoctorId;
        target.PatientId = source.PatientId;
        target.Comment = source.Comment;
        target.Score = source.Score;
        target.CreatedAt = source.CreatedAt;
    }
    public static TimeSlot Map(this TimeSlotRequestDTO timeSlotRequest)
    {
        return new TimeSlot
        {
            Id = Guid.NewGuid(),
            DoctorId = timeSlotRequest.DoctorId,
            AvailableDay = timeSlotRequest.AvailableDay,
            AppointmentFrequency = timeSlotRequest.AppointmentFrequency,
            AvailableFrom = timeSlotRequest.AvailableFrom,
            AvailableTo = timeSlotRequest.AvailableTo,
        };
    }
    public static void Map(this TimeSlotRequestDTO source, TimeSlot target)
    {
        target.AvailableFrom = source.AvailableFrom;
        target.AppointmentFrequency = source.AppointmentFrequency;
        target.AvailableDay = source.AvailableDay;
        target.AvailableTo = source.AvailableTo;
        target.DoctorId = source.DoctorId;
    }
}
