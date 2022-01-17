using FluentValidation;
using FM.Application.Constants;
using FM.Application.DTOs.Requests.Accounts;

namespace FM.Application.Validations
{
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage(AppConstant.PROPERTY_NAME_SHOULD_NOT_BE_EMPTY)
                .EmailAddress().WithMessage(AppConstant.A_VALID_EMAIL_IS_REQUIRED);

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage(AppConstant.PROPERTY_NAME_SHOULD_NOT_BE_EMPTY);
        }
    }
}