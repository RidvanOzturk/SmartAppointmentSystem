namespace SmartAppointmentSystem.Business.DTOs.RequestDTOs;

public class TokenRequest
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Mail { get; set; }

};
