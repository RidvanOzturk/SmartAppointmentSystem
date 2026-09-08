namespace SmartAppointmentSystem.Business.DTOs.AI;

public sealed record AppointmentSearchIntent(
    string? Branch,
    DateOnly? Date,
    TimeOnly? AfterTime
);