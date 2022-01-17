using System;
using System.Threading.Tasks;
using FM.Application.DTOs.Requests.Accounts;
using FM.Application.DTOs.Responses.Accounts;
using FM.Application.Wrappers.Responses;
using FM.Domain.Entities.Identity;

namespace FM.Application.Interfaces.Shared
{
    public interface IAccountService
    {
        Task<BaseResponse<string>> Register(RegisterRequest request);

        Task<BaseResponse<AuthenticationResponse>> Authenticate(AuthenticateRequest request, string ipAddress);

        Task<BaseResponse<bool>> VerifyEmail(string token, string userId);

        Task<BaseResponse<bool>> RevokeToken(string token, ApplicationUser currentUser, string ipAddress);

        Task<BaseResponse<AuthenticationResponse>> RefreshToken(string token, string ipAddress);

        Task<BaseResponse<bool>> ForgotPassword(ForgotPasswordRequest request);

        Task<BaseResponse<ValidateResetTokenResponse>> ValidateResetToken(ValidateResetTokenRequest request);

        Task<BaseResponse<string>> ResetPassword(ResetPasswordRequest request);

        Task<BaseResponse<AccountResponse>> GetById(Guid id);

        Task<BaseResponse<AccountResponse>> Create(CreateRequest request);
    }
}