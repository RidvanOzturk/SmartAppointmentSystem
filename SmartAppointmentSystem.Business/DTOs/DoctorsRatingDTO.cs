namespace SmartAppointmentSystem.Business.DTOs;

public class DoctorsRatingDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public int BranchId { get; set; }
    public double AverageRating { get; set; }
}
