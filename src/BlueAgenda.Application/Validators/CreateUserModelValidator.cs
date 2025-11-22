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
            .Must(CpfValidator.IsValid)
            .Matches(@"^\d{11}$");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches("[a-z]")
            .Matches("[^a-zA-Z0-9]");

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.Today);
    }
}
