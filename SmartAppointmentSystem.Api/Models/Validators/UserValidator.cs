using FluentValidation;
using FluentValidation.Validators;
using SmartAppointmentSystem.Data.Entities;
using System.ComponentModel.DataAnnotations;
namespace SmartAppointmentSystem.Api.Models.Validators;

public class UserValidator : AbstractValidator<UserRequestModel>
{
    public UserValidator()
    {
        RuleFor(x => x.Name).MaximumLength(10).MinimumLength(3);
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty.").EmailAddress().WithMessage("Wrong format for Email");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Email cannot be empty.").MinimumLength(5).MaximumLength(12);
    }
}
