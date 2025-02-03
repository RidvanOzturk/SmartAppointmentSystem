using FluentValidation;

namespace SmartAppointmentSystem.Api.Models.Validators
{
    public class DoctorUserValidator : AbstractValidator<DoctorUserRequestModel>
    {
        public DoctorUserValidator()
        {
            RuleFor(x => x.Name).MaximumLength(10).MinimumLength(3).NotEmpty().NotNull();
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty.").EmailAddress().WithMessage("Wrong format for Email");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Email cannot be empty.").MinimumLength(4).MaximumLength(15);
        }
    }

}
