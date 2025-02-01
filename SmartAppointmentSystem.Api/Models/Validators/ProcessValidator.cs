using FluentValidation;

namespace SmartAppointmentSystem.Api.Models.Validators
{
    public class ProcessValidator : AbstractValidator<ProcessRequestModel>
    {
        public ProcessValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().MinimumLength(3).MaximumLength(35);
            RuleFor(x => x.Duration).NotEmpty().GreaterThan(1);
            RuleFor(x => x.DoctorId).NotNull().NotEmpty();
        }
    }
}
