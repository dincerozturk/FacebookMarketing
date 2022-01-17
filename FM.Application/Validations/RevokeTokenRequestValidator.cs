using FluentValidation;
using FM.Application.Constants;
using FM.Application.DTOs.Requests.Accounts;

namespace FM.Application.Validations
{
    public class RevokeTokenRequestValidator : AbstractValidator<RevokeTokenRequest>
    {
        public RevokeTokenRequestValidator()
        {
            RuleFor(p => p.Token)
                .NotEmpty().WithMessage(AppConstant.PROPERTY_NAME_SHOULD_NOT_BE_EMPTY);
        }
    }
}