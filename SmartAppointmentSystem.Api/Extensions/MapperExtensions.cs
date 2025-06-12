using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.DTOs;
using System.Text;

namespace SmartAppointmentSystem.Api.Extensions;

public static class MapperExtensions
{
    public static PatientUserRequestDTO Map(this PatientUserRequestModel value)
    {
        return new PatientUserRequestDTO(value.Name, value.Email, value.Password);
    }
    public static DoctorUserRequestDTO Map(this DoctorUserRequestModel model)
    {
        return new DoctorUserRequestDTO(
            model.Name,
            model.Email,
            model.Password,
            model.Description,
            model.Image,
            model.BranchId
        );
    }

    public static AppointmentRequestDTO Map(this AppointmentRequestModel value, Guid patientId)
    {
        return new AppointmentRequestDTO(
            value.DoctorId,
            patientId,
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
    public static LogDTO ToLogDTO(this HttpContext context, string requestBody, string responseBody)
    {
        var request = context.Request;
        var headers = string.Join(Environment.NewLine, request.Headers.Select(h => $"{h.Key}: {h.Value}"));

        return new LogDTO(
            Id: Guid.NewGuid(),
            Request: requestBody,
            Headers: headers,
            Endpoint: request.Path.ToString(),
            HttpMethod: request.Method,
            StatusCode: context.Response.StatusCode.ToString(),
            Response: responseBody,
            IP: context.Connection.GetIPAddress(),
            CreatedAt: DateTime.UtcNow
        );
    }
}
