using BlueAgenda.Application.Models;
using FluentValidation;

namespace BlueAgenda.Application.Validators;

public class CreateUserModelValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\d{10,11}$");

        RuleFor(x => x.Cpf)
            .NotEmpty()
            .Length(11);

        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.BirthDate)
            .NotEmpty();
    }
}
