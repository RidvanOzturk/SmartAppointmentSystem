namespace SmartAppointmentSystem.Api.Models;

public class TimeSlotRequestModel
{
    public Guid DoctorId { get; set; }
    public int AvailableDay { get; set; }
    public int AppointmentFrequency { get; set; }
    public TimeSpan AvailableFrom { get; set; }
    public TimeSpan AvailableTo { get; set; }

}
