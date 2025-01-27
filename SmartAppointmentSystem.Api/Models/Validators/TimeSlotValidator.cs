using FluentValidation;

namespace SmartAppointmentSystem.Api.Models.Validators
{
    public class TimeSlotValidator : AbstractValidator<TimeSlotRequestModel>
    {
        public TimeSlotValidator()
        {
            RuleFor(x => x.AvailableFrom).NotNull().NotEmpty();
            RuleFor(x => x.AvailableTo).NotNull().NotEmpty();
            RuleFor(x => x.ProfessionalId).NotNull().NotEmpty();
        }
    }
}
