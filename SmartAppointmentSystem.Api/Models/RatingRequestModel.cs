﻿namespace SmartAppointmentSystem.Api.Models;

public class RatingRequestModel
{
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public int Score { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}
