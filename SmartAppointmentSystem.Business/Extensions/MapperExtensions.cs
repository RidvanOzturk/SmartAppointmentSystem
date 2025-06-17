using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.DTOs.RequestDTOs;
using SmartAppointmentSystem.Data.Entities;

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
            Time = createRequest.Time,
            Notes = createRequest.Notes,
            Status = createRequest.Status,
            DoctorId = createRequest.DoctorId,
        };
    }
    public static void Map(this AppointmentRequestDTO createRequest, Appointment target)
    {
        target.PatientId = createRequest.PatientId;
        target.Time = createRequest.Time;
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
    public static Doctor Map(this DoctorUserSignUpRequestDTO doctorUserSignUpRequest)
    {
        return new Doctor
        {
            Id = Guid.NewGuid(),
            Name = doctorUserSignUpRequest.Name,
            PasswordHash = doctorUserSignUpRequest.Password,
            Email = doctorUserSignUpRequest.Email
        };
    }
    public static void Map(this DoctorsRatingDTO target, Doctor source)
    {
        target.Id = source.Id;
        target.Name = source.Name;
        target.Email = source.Email;
        target.Description = source.Description;
        target.BranchId = source.BranchId.Value;
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
    public static Log Map(this LogDTO logDTO)
    {
        return new Log
        {
            Id = Guid.NewGuid(),
            Request = logDTO.Request,
            Headers = logDTO.Headers,
            Endpoint = logDTO.Endpoint,
            HttpMethod = logDTO.HttpMethod,
            StatusCode = logDTO.StatusCode,
            Response = logDTO.Response,
            Ip = logDTO.IP,
            CreatedAt = logDTO.CreatedAt,
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
