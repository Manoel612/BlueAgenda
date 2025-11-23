using BlueAgenda.Application.Models;
using FluentValidation;

namespace BlueAgenda.Application.Validators;

public class CreateContactModelValidator : AbstractValidator<CreateContactModel>
{
    public CreateContactModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("Email inválido");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\d{10,11}$");

        RuleFor(x => x.BirthDate)
            .LessThanOrEqualTo(DateTime.Today);
    }
}
