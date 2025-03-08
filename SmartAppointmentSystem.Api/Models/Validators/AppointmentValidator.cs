using FluentValidation;

namespace SmartAppointmentSystem.Api.Models.Validators;

public class AppointmentValidator : AbstractValidator<AppointmentRequestModel>
{
    public AppointmentValidator()
    {
        RuleFor(x => x.Status).NotEmpty().NotNull();
        RuleFor(x => x.DoctorId).NotEmpty().NotNull();
        RuleFor(x => x.Time)
            .NotNull()
            .Must(x => x > DateTime.Now)
            .WithMessage("The appointment time must be in the future.")
            .Must(x => !(x.TimeOfDay >= new TimeSpan(12, 0, 0) && x.TimeOfDay < new TimeSpan(13, 0, 0)))
            .WithMessage("Appointments cannot be scheduled between 12:00 and 13:00.");
        RuleFor(x => x.Notes).Empty(); 
    }
}
