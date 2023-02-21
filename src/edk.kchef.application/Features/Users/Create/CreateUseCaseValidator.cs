using edk.Fusc.Contracts;
using edk.Fusc.Core.Validators;
using FluentValidation;
using System.Collections.Generic;

namespace edk.Kchef.Application.Features.Users.Create
{
    public class CreateUseCaseValidator: AbstractValidator<CreateUserInput>, IUseCaseValidator<CreateUserInput>
    {
        public CreateUseCaseValidator()
        {
            RuleFor(e => e.Login).NotEmpty().WithMessage("O {PropertyName} é obrigatório.");
            RuleFor(e => e.Email).NotEmpty().WithMessage("O {PropertyName} é obrigatório.");
            RuleFor(e => e.Password).NotEmpty().WithMessage("O {PropertyName} é obrigatório.");
            RuleFor(e => e.Email).NotEmpty().EmailAddress().WithMessage("O {PropertyName} é obrigatório.");
        }


        public IReadOnlyCollection<INotification> Validate(CreateUserInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}