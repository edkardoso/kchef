﻿using edk.Fusc.Contracts;
using edk.Fusc.Core.Validators;
using edk.Kchef.Application.Common;
using edk.Kchef.Domain.Common;
using edk.Kchef.Domain.Common.Resources;
using FluentValidation;
using System.Collections.Generic;

namespace edk.Kchef.Application.Features.Users.Create
{
    public class CreateUseCaseValidator : AbstractValidator<CreateUserInput>, IUseCaseValidator<CreateUserInput>
    {
        private const int MINIMUM_LENGTH = 3;
        

        public CreateUseCaseValidator()
        {
            RuleFor(e => e.Login)
                .NotEmpty().WithMessage(string.Format(UserResource.PropertyRequired, Setup.TAG_PROPERTY))
                .MinimumLength(MINIMUM_LENGTH).WithMessage(string.Format(UserResource.PropertyRangeLength, Setup.TAG_PROPERTY, MINIMUM_LENGTH, SizeFields.SIZE_4))
                .MaximumLength(SizeFields.SIZE_4).WithMessage(string.Format(UserResource.PropertyRangeLength, Setup.TAG_PROPERTY, MINIMUM_LENGTH, SizeFields.SIZE_4));

            RuleFor(e => e.FirstName)
                .NotEmpty().WithMessage(string.Format(UserResource.PropertyRequired, Setup.TAG_PROPERTY))
                .MinimumLength(MINIMUM_LENGTH).WithMessage(string.Format(UserResource.PropertyRangeLength, Setup.TAG_PROPERTY, MINIMUM_LENGTH, SizeFields.SIZE_3))
                .MaximumLength(SizeFields.SIZE_3).WithMessage(string.Format(UserResource.PropertyRangeLength, Setup.TAG_PROPERTY, MINIMUM_LENGTH, SizeFields.SIZE_3));

            RuleFor(e => e.Password)
                .NotEmpty().WithMessage(string.Format(UserResource.PropertyRequired, Setup.TAG_PROPERTY));

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage(string.Format(UserResource.PropertyRequired, Setup.TAG_PROPERTY))
                .EmailAddress().WithMessage(UserResource.InvalidEmail)
                .MaximumLength(SizeFields.SIZE_6).WithMessage(string.Format(UserResource.PropertyMaxSize, Setup.TAG_PROPERTY, SizeFields.SIZE_6));

        }

        public new IReadOnlyCollection<INotification> Validate(CreateUserInput input)
        {
            var validationResult = base.Validate(input);
            return validationResult.Errors.ToNotifications();
        }
    }
}