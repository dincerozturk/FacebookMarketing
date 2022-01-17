using FM.Domain.Entities.Identity;

namespace FM.Application.DTOs.Responses.Accounts
{
    public class GetRefreshTokenResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public RefreshToken RefreshToken { get; set; }
    }
}