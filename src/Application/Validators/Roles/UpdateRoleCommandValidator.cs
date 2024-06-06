using Application.Commands.Roles;
using Domain.Interface;
using FluentValidation;

namespace Application.Validators.Roles
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        private readonly IRoleRepository _repository;

        public UpdateRoleCommandValidator(IRoleRepository repository)
        {
            _repository = repository;


            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        }

    }
}
