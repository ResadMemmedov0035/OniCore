using FluentValidation;
using KodlamaDevs.Application.Features.Developers.Commands;

namespace KodlamaDevs.Application.Features.Developers.Validators
{
    public class LoginDeveloperCommandValidator : AbstractValidator<LoginDeveloperCommand>
    {
        public LoginDeveloperCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(8);
        }
    }
}
