using FluentValidation;
using KodlamaDevs.Application.Features.Developers.Commands;

namespace KodlamaDevs.Application.Features.Developers.Validators
{
    public class RegisterDeveloperCommandValidator : AbstractValidator<RegisterDeveloperCommand>
    {
        public RegisterDeveloperCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.FirstName).MinimumLength(2);
            RuleFor(x => x.FirstName).MaximumLength(50);

            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.LastName).MinimumLength(2);
            RuleFor(x => x.LastName).MaximumLength(50);

            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(8);

            RuleFor(x => x.GithubAddress).NotEmpty();

        }
    }
}
