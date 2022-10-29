using FluentValidation;
using KodlamaDevs.Application.Features.Developers.Commands;

namespace KodlamaDevs.Application.Features.Developers.Validators
{
    public class RegisterDeveloperCommandValidator : AbstractValidator<RegisterDeveloperCommand>
    {
        public RegisterDeveloperCommandValidator()
        {
            RuleFor(x => x.Model.FirstName).NotEmpty();
            RuleFor(x => x.Model.FirstName).MinimumLength(2);
            RuleFor(x => x.Model.FirstName).MaximumLength(50);

            RuleFor(x => x.Model.LastName).NotEmpty();
            RuleFor(x => x.Model.LastName).MinimumLength(2);
            RuleFor(x => x.Model.LastName).MaximumLength(50);

            RuleFor(x => x.Model.Email).NotEmpty();
            RuleFor(x => x.Model.Email).EmailAddress();

            RuleFor(x => x.Model.Password).NotEmpty();
            RuleFor(x => x.Model.Password).MinimumLength(8);

            RuleFor(x => x.Model.GithubAddress).NotEmpty();
        }
    }
}
