using Microsoft.AspNetCore.Http;

namespace FM.Infrastructure.Shared.Helpers
{
    public static class HttpContextHelper
    {
        public static string GenerateConfirmLink(IHttpContextAccessor accessor, string verifyToken, string userId)
        {
            var scheme = accessor?.HttpContext?.Request.Scheme;
            var host = accessor?.HttpContext?.Request.Host;

            return $"{scheme}://{host}/api/accounts/verify-email?token={verifyToken}&userId={userId}";
        }

        public static string GenerateResetPasswordLink(IHttpContextAccessor accessor, string resetPasswordToken, string userId)
        {
            var scheme = accessor?.HttpContext?.Request.Scheme;
            var host = accessor?.HttpContext?.Request.Host;

            return $"{scheme}://{host}/api/accounts/reset-password?token={resetPasswordToken}&userId={userId}";
        }
    }
}