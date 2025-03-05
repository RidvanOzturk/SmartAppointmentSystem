using FluentValidation;

namespace SmartAppointmentSystem.Api.Models.Validators
{
    public class RatingValidator : AbstractValidator<RatingRequestModel>
    {
        public RatingValidator() 
        {
            RuleFor(x => x.Score).GreaterThan(0).LessThan(6).WithMessage("The score must be 5 between 1");
            RuleFor(x=>x.Comment).MinimumLength(3).MinimumLength(30).WithMessage("The comment must be greater than 3, lower than 30 characters");
        }
    }
}
