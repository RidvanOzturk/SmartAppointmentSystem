namespace SmartAppointmentSystem.Business.DTOs;

public class TokenRequest
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Mail { get; set; }

};
