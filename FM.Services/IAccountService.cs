using System.Threading.Tasks;
using FM.Application.DTOs.Requests.Accounts;
using FM.Application.DTOs.Responses.Accounts;
using FM.Application.Wrappers;
using FM.Domain.Entities;

namespace FM.Services
{
    public interface IAccountService
    {
        //Task<string> RegisterAsync(RegisterModel model);
        Task<BaseResponse<string>> RegisterAsync(RegisterModelRequest model);

        Task<BaseResponse<AuthenticationResponse>> GetTokenAsync(TokenModelRequest model);

        Task<string> AddRoleAsync(AddRoleModelRequest model);
        Task<string> UpdateRoleAsync(UpdateRoleModelRequest model);

        Task<AuthenticationResponse> RefreshTokenAsync(string token);

        ApplicationUser GetById(string id);

        Task<bool> RevokeToken(string token);
    }
}
