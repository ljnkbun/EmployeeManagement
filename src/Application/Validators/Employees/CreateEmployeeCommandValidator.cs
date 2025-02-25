﻿using Application.Commands.Employees;
using Domain.Interface;
using FluentValidation;

namespace Application.Validators.Employees
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        private readonly IEmployeeRepository _repository;

        public CreateEmployeeCommandValidator(IEmployeeRepository repository)
        {
            _repository = repository;

            RuleFor(p => p.Code)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
               .MustAsync(IsUniqueCodeAsync).WithMessage("{PropertyName} must unique.");

            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                .MustAsync(IsUniqueAsync).WithMessage("{PropertyName} must unique.");

            RuleFor(p => p.Password)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(5000).WithMessage("{PropertyName} must not exceed 50 characters.");


            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        }

        private async Task<bool> IsUniqueAsync(string username, CancellationToken cancellationToken)
        {
            return await _repository.IsUniqueAsync(username);
        }

        private async Task<bool> IsUniqueCodeAsync(string code, CancellationToken cancellationToken)
        {
            return await _repository.IsUniqueCodeAsync(code);
        }
    }
}
