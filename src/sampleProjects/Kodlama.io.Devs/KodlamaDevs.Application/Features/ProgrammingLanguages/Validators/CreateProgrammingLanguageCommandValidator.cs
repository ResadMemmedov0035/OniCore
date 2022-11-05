using FluentValidation;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Commands;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Validators;

public class CreateProgrammingLanguageCommandValidator : AbstractValidator<CreateProgrammingLanguageCommand>
{
    public CreateProgrammingLanguageCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MinimumLength(2);
        RuleFor(x => x.Name).MaximumLength(50);
    }
}
