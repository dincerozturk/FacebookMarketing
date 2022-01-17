using System.Collections.Generic;
using System.Threading.Tasks;
using FM.Application.DTOs.Requests.Facebook.Groups;
using FM.Application.Wrappers.Responses;
using FM.Domain.Entities.Facebook;
using Microsoft.AspNetCore.Hosting;

namespace FM.Application.Interfaces.Shared
{
    public interface IGroupService
    {
        Task<BaseResponse<List<FbPostGroup>>> GetAllPostOfGroup(GetAllPostOfGroupRequest request);
        
        Task<BaseResponse<string>> GenerateFbTokens(IWebHostEnvironment webHostEnvironment);
    }
}