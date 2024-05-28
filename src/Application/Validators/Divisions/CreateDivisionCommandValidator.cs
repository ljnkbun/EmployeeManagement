using Application.Commands.Divisions;
using Domain.Interface;
using FluentValidation;

namespace Application.Validators.Divisions
{
    public class CreateDivisionCommandValidator : AbstractValidator<CreateDivisionCommand>
    {

        public CreateDivisionCommandValidator()
        {

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        }

    }
}
