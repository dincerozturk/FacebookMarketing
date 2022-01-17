using FluentValidation;
using FM.Application.Constants;
using FM.Application.DTOs.Requests.Accounts;

namespace FM.Application.Validations
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(p => p.Token)
                .NotEmpty().WithMessage(AppConstant.PROPERTY_NAME_SHOULD_NOT_BE_EMPTY);


            RuleFor(p => p.Password)
                .NotEmpty().WithMessage(AppConstant.PROPERTY_NAME_SHOULD_NOT_BE_EMPTY);


            RuleFor(p => p.ConfirmPassword)
                .NotEmpty().WithMessage(AppConstant.PROPERTY_NAME_SHOULD_NOT_BE_EMPTY);

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.Password), AppConstant.PASSWORD_DOEST_NOT_MATCH);
                }
            });
        }
    }
}