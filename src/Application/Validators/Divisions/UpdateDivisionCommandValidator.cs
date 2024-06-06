using Application.Commands.Divisions;
using Domain.Interface;
using FluentValidation;

namespace Application.Validators.Divisions
{
    public class UpdateDivisionCommandValidator : AbstractValidator<UpdateDivisionCommand>
    {
        private readonly IDivisionRepository _repository;

        public UpdateDivisionCommandValidator(IDivisionRepository repository)
        {

            _repository = repository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }

    }
}
