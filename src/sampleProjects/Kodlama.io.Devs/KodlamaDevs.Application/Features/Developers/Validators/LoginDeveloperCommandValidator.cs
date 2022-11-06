using FluentValidation;
using KodlamaDevs.Application.Features.Developers.Commands;

namespace KodlamaDevs.Application.Features.Developers.Validators
{
    public class LoginDeveloperCommandValidator : AbstractValidator<LoginDeveloperCommand>
    {
        public LoginDeveloperCommandValidator()
        {
            RuleFor(x => x.Model.Email).NotEmpty();
            RuleFor(x => x.Model.Email).EmailAddress();

            RuleFor(x => x.Model.Password).NotEmpty();
            RuleFor(x => x.Model.Password).MinimumLength(8);
        }
    }
}
