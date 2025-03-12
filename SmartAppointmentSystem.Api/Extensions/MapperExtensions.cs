using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.DTOs;

namespace SmartAppointmentSystem.Api.Extensions;

public static class MapperExtensions
{
    public static PatientUserRequestDTO Map(this PatientUserRequestModel value)
    {
        return new PatientUserRequestDTO(value.Name, value.Email, value.Password);
    }

    public static AppointmentRequestDTO Map(this AppointmentRequestModel value)
    {
        return new AppointmentRequestDTO(
            value.DoctorId,
            value.PatientId,
            value.Time,
            value.Notes,
            value.Status
        );
    }

    public static RatingRequestDTO Map(this RatingRequestModel value)
    {
        return new RatingRequestDTO(
            value.DoctorId,
            value.PatientId,
            value.Score,
            value.Comment,
            value.CreatedAt
        );
    }

    public static TimeSlotRequestDTO Map(this TimeSlotRequestModel value)
    {
        return new TimeSlotRequestDTO(
            value.DoctorId,
            value.AvailableDay,
            value.AppointmentFrequency,
            value.AvailableFrom,
            value.AvailableTo
        );
    }

    public static DoctorUserRequestDTO Map(this DoctorUserRequestModel value)
    {
        return new DoctorUserRequestDTO(
            value.Name,
            value.Email,
            value.Password,
            value.Description,
            null,
            value.BranchId
        );
    }

    public static DoctorUserSignUpRequestDTO Map(this DoctorUserSignUpRequestModel value)
    {
        return new DoctorUserSignUpRequestDTO(value.Name, value.Email, value.Password);
    }

    public static DoctorUserLoginRequestDTO Map(this DoctorUserLoginRequestModel value)
    {
        return new DoctorUserLoginRequestDTO(value.Email, value.Password);
    }

    public static PatientUserLoginRequestDTO Map(this PatientUserLoginRequestModel value)
    {
        return new PatientUserLoginRequestDTO(value.Email, value.Password);
    }
    public static LogDTO ToLogDTO(this HttpContext context)
    {
        var request = context.Request;
        var endpoint = request.Path.ToString();
        var method = request.Method;
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var headers = string.Join("\n", request.Headers.Select(h => $"{h.Key}: {h.Value}"));
        var statusCode = context.Response.StatusCode.ToString();

        return new LogDTO(
            Guid.NewGuid(),
            $"{method} {endpoint}",
            headers,
            endpoint,
            method,
            statusCode,
            ip,
            DateTime.UtcNow
        );
    }
}
