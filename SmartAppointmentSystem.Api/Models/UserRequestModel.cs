﻿namespace SmartAppointmentSystem.Api.Models;

public class UserRequestModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }

    public string? Role { get; set; }
}
