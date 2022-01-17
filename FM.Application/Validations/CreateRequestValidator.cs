using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FM.Application.Constants;
using FM.Application.DTOs.Requests.Accounts;
using FM.Domain.Enums;

namespace FM.Application.Validations
{
    public class CreateRequestValidator : AbstractValidator<CreateRequest>
    {
        public CreateRequestValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage(AppConstant.PROPERTY_NAME_SHOULD_NOT_BE_EMPTY);

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage(AppConstant.PROPERTY_NAME_SHOULD_NOT_BE_EMPTY);

            RuleFor(p => p.Roles).Must(RoleCheckValidator)
                .WithMessage(AppConstant.A_VALID_ROLE_IS_REQUIRED);

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage(AppConstant.PROPERTY_NAME_SHOULD_NOT_BE_EMPTY)
                .EmailAddress().WithMessage(AppConstant.A_VALID_EMAIL_IS_REQUIRED);

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage(AppConstant.PROPERTY_NAME_SHOULD_NOT_BE_EMPTY);
        }

        private bool RoleCheckValidator(List<string> roles)
        {
            var allRolesOfApp = Enum.GetValues(typeof(Roles))
                    .Cast<Roles>()
                    .Select(v => v.ToString())
                    .ToList();

            return roles.All(x => allRolesOfApp.Contains(x));
        }
    }
}