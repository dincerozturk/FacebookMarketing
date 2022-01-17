using FM.Application.Constants;
using FM.Application.DTOs.Requests.Facebook.Groups;
using FM.Application.Exceptions;
using FM.Application.Interfaces.Repositories;
using FM.Application.Interfaces.Shared;
using FM.Application.Utilities;
using FM.Application.Wrappers.Responses;
using FM.Domain.Entities.Facebook;
using FM.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FM.API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class GroupsController : BaseController
    {
        private readonly IGroupService _groupService;
        private readonly IGroupRepository _groupRepository;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public GroupsController(UserManager<ApplicationUser> userManager, IGroupService groupService, IWebHostEnvironment webHostEnvironment, IGroupRepository groupRepository) : base(userManager)
        {
            _groupService = groupService;
            _webHostEnvironment = webHostEnvironment;
            _groupRepository = groupRepository;
        }

        [HttpGet("Token")]
        public async Task<IActionResult> GenerateFbTokens()
        {
            try
            {
                var result = await _groupService.GenerateFbTokens(_webHostEnvironment);
                return Ok(result);
            }
            catch (Exception e)
            {
                throw new ExceptionCustom("GenerateFbTokens", null, e.Message, e);
            }
        }


        //Call facebook api and insert to db
        [HttpPost]
        public async Task<IActionResult> GetAllPostGroup([FromBody] GetAllPostOfGroupRequest request)
        {
            var result = await _groupService.GetAllPostOfGroup(request);
            return Ok(result);
        }
        
        [HttpGet("get-posts")]
        public async Task<IActionResult> GetPosts(string searchValue = "", int pageIndex = AppConstant.PageIndex,
            int pageSize = AppConstant.PageSize, SortType sortType = SortType.Descending)
        {
            var fbPostGroups = await _groupRepository.GetWithPagingAsync(
               !string.IsNullOrEmpty(searchValue) ? (x => x.Message.Contains(searchValue)) : null,
                null,
                sortType,
                pageIndex,
                pageSize);

            var response = new BaseResponse<IEnumerable<FbPostGroup>>(true, AppConstant.SUCCESS, null, fbPostGroups);

            return response.GetResponse(response);
        }
    }
}