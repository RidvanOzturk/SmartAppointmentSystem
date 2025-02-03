using FluentValidation;

namespace SmartAppointmentSystem.Api.Models.Validators
{
    public class AppointmentValidator : AbstractValidator<AppointmentRequestModel>
    {
        public AppointmentValidator()
        {
            RuleFor(x => x.Status).NotEmpty().NotNull();
            RuleFor(x=>x.DoctorId).NotEmpty().NotNull();
            RuleFor(x=>x.TimeSlotId).NotNull().NotEmpty();
            RuleFor(x => x.Notes).Null().Empty();
        }
    }
}
