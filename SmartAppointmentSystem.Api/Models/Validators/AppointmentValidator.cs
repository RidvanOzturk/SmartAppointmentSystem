using FluentValidation;

namespace SmartAppointmentSystem.Api.Models.Validators
{
    public class AppointmentValidator : AbstractValidator<AppointmentRequestModel>
    {
        public AppointmentValidator()
        {
            RuleFor(x => x.Status).NotEmpty().NotNull();
            RuleFor(x=>x.ProfessionalId).NotEmpty().NotNull();
            RuleFor(x=>x.DateTime).NotNull().NotEmpty();
            RuleFor(x => x.Notes).Null().Empty();
        }
    }
}
