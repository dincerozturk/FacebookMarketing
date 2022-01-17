using AutoMapper;
using FM.Application.DTOs.Requests.Facebook.Groups;
using FM.Application.DTOs.Responses.Facebook.Groups;
using FM.Application.Exceptions;
using FM.Application.Interfaces.Shared;
using FM.Application.Wrappers.Responses;
using FM.Domain.Entities.Facebook;
using FM.Infrastructure.Contexts;
using FM.Infrastructure.Seeds;
using FM.Infrastructure.Shared.Helpers.AppHelpers;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using FM.Application.Constants;
using FM.Infrastructure.Shared.Helpers;
using Z.BulkOperations;

namespace FM.Infrastructure.Shared.Services
{
    public class GroupService : IGroupService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public GroupService(HttpClient httpClient, IMapper mapper, ApplicationDbContext context)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _context = context;
        }

        public async Task<BaseResponse<List<FbPostGroup>>> GetAllPostOfGroup(GetAllPostOfGroupRequest request)
        {
            var result = new List<FbPostGroup>();
            PostGroupRoot root;

            do
            {
                await Task.Delay(1000);
                try
                {
                    var response = await GetResponsePriority(_httpClient, _context, request);

                    root = JsonConvert.DeserializeObject<PostGroupRoot>(response);
                    if (root == null || root.Data == null || response == "")
                    {
                        await GetResponsePriority(_httpClient, _context, request);
                        LogHelper.WriteLog($"Root is null, request={JsonConvert.SerializeObject(request)}");
                    } else if (root.Data != null && root.Paging == null && root.Data.Count == 0)
                    {
                        return new BaseResponse<List<FbPostGroup>>(true, "Finished!", null, result);
                    }

                    await InsertPostGroup(root);

                    request = request with
                    {
                        Since = HttpUtility.ParseQueryString(new Uri(root.Paging.Next).Query).Get("since"),
                        Until = HttpUtility.ParseQueryString(new Uri(root.Paging.Next).Query).Get("until"),
                        PagingToken = HttpUtility.ParseQueryString(new Uri(root.Paging.Next).Query).Get("__paging_token")
                    };
                }
                catch (Exception e)
                {
                    throw new ExceptionCustom("GetAllPostOfGroup", request, e.Message, e);
                }
            } while (root.Data.Any());

            return new BaseResponse<List<FbPostGroup>>(result);
        }

        public async Task<BaseResponse<string>> GenerateFbTokens(IWebHostEnvironment webHostEnvironment)
        {
            try
            {
                var listToken = ExcelUtil.MakeListFbTokensFromExcelFile(webHostEnvironment, "FbTokens.xlsx");
                await SeedData.SeedDefaultToken(listToken, _context);

                return new BaseResponse<string>(true, AppConstant.SUCCESS, null, listToken.Count.ToString(), (int)HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                LogHelper.WriteLog($"Exception in GenerateFbTokens, Exception = {e.Message}, InnerException = {e.InnerException?.Message}");
                throw new ExceptionCustom("GenerateFbTokens", null, e.Message, e);
            }
        }

        #region Helper methods
        private async Task InsertPostGroup(PostGroupRoot postGroupRoot)
        {
            try
            {
                var list = _mapper.Map<List<FbPostGroup>>(postGroupRoot.Data);

                await _context.BulkMergeAsync(list, options =>
                {
                    options.IncludeGraph = true;
                    options.IncludeGraphOperationBuilder = operation =>
                    {
                        if (operation is BulkOperation<FbPostGroup>)
                        {
                            var bulk = (BulkOperation<FbPostGroup>)operation;
                            bulk.ColumnPrimaryKeyExpression = x => new { x.FbId };
                        }

                        else if (operation is BulkOperation<FbPostGroupAction>)
                        {
                            var bulk = (BulkOperation<FbPostGroupAction>)operation;
                            bulk.ColumnPrimaryKeyExpression = x => new { x.Name, x.Link, x.PostGroupId };
                        }

                        else if (operation is BulkOperation<FbPostGroupComment>)
                        {
                            var bulk = (BulkOperation<FbPostGroupComment>)operation;
                            bulk.ColumnPrimaryKeyExpression = x => new { x.FbId };
                        }

                        else if (operation is BulkOperation<FbPostGroupPrivacy>)
                        {
                            var bulk = (BulkOperation<FbPostGroupPrivacy>)operation;
                            bulk.AllowDuplicateKeys = true;
                            bulk.ColumnPrimaryKeyExpression = x => new { x.Allow, x.Deny, x.Description, x.Friends, x.Value, x.PostGroupId };
                        }
                    };
                });
            }
            catch (Exception e)
            {
                throw new ExceptionCustom("InsertPostGroup", postGroupRoot, e.Message, e);
            }
        }

        private static async Task<FbToken> GetTokenUse(ApplicationDbContext context)
        {
            try
            {
                int minuteInTimeFrame = 2;
                var dtNow = DateTime.Now;
                var dtCheck = DateTime.Now.AddMinutes(-minuteInTimeFrame);

                var listToken = context.FbTokens.Where(x =>
                        !x.IsDead && !x.IsUsing && (x.TotalCalledInTimeFrame < x.MaxRequestInTimeFrame || (x.TotalCalledInTimeFrame >= x.MaxRequestInTimeFrame && dtCheck >= x.LastCalledTime)))
                    .ToList();

                if (listToken.Count > 0)
                {
                    var result = ListHelper<FbToken>.GetRandomItemInListObject(listToken);
                    if (result != null)
                    {
                        result.IsUsing = true;

                        var dtFromCheck = new DateTime(result.LastCalledTime.Year, result.LastCalledTime.Month, result.LastCalledTime.Day, result.LastCalledTime.Hour, 0, 0);

                        if (dtFromCheck < DateTime.Now && DateTime.Now < dtFromCheck.AddHours(1))
                        {
                            result.TotalCalledInLastHour += 1;
                        }
                        else
                        {
                            result.TotalCalledInLastHour = 1;
                        }

                        if (dtNow >= result.LastCalledTime.AddMinutes(2))
                        {
                            result.TotalCalledInTimeFrame = 1;
                        }
                        else
                        {
                            result.TotalCalledInTimeFrame = result.TotalCalledInTimeFrame + 1;
                        }

                        result.LastCalledTime = DateTime.Now;
                        result.TotalCalled = result.TotalCalled + 1;

                        await context.SaveChangesAsync();
                        return result;
                    }
                }
                else
                {
                    string body = $"Cookie chạy tool die hết rồi!!! - {DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy")}";
                    await LogHelper.WriteLogAsync(body);
                }
            }
            catch (Exception e)
            {
                await LogHelper.WriteLogAsync($"Exception in GetCookieUser, Exception={e.Message}, InnerException={e.InnerException?.Message}");
            }

            return null;
        }

        private static async Task ReleaseTokenUse(ApplicationDbContext context, long id, bool isDead, bool isSentryBlock = false)
        {
            try
            {
                var tokenInDb = context.FbTokens.FirstOrDefault(x => x.Id == id);
                if (tokenInDb != null)
                {
                    tokenInDb.IsUsing = false;
                    tokenInDb.IsDead = isDead;
                    if (isSentryBlock)
                    {
                        tokenInDb.TotalCalledInLastHour = tokenInDb.MaxRequestInTimeFrame;
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                await LogHelper.WriteLogAsync($"Exception in ReleaseCookieUse, Exception={e.Message}, InnerException={e.InnerException?.Message}");
            }
        }

        private async Task<string> GetResponsePriority(HttpClient httpClient, ApplicationDbContext dbContext, GetAllPostOfGroupRequest request)
        {
            FbToken accessToken = await GetTokenUse(dbContext);
            request = request with
            {
                AccessToken = accessToken.Token
            };

            string resultAfterCallApi = "";
            try
            {
                var response = await httpClient.GetAsync(request.UrlGet);
                resultAfterCallApi = await response.Content.ReadAsStringAsync();

                return await CheckResponseAfterCallApi(resultAfterCallApi, httpClient, dbContext, accessToken, request);
            }
            catch (Exception e)
            {
                await CheckResponseAfterCallApi(resultAfterCallApi, httpClient, dbContext, accessToken, request);
                throw new ExceptionCustom("GetResponsePriority", request, e.Message, e);
            }
        }

        private async Task<string> CheckResponseAfterCallApi(string response, HttpClient httpClient, ApplicationDbContext dbContext, FbToken accessToken, GetAllPostOfGroupRequest request)
        {
            try
            {
                if (response.Contains("\"code\": 190") || response.Contains("\"code\":190"))
                {
                    await ReleaseTokenUse(dbContext, accessToken.Id, true, false); //Set lại token - đánh dấu dead
                    return await GetResponsePriority(httpClient, dbContext, request);
                }
                if ((response.Contains("Object with ID") && response.Contains("does not exist, cannot be loaded due to missing permissions")) || response.Contains("\"error_subcode\": 2018218"))
                {

                    await ReleaseTokenUse(_context, accessToken.Id, false, false); ; //Set lại token
                }
                else if (response.Contains("sentry_block_data"))
                {
                    await ReleaseTokenUse(_context, accessToken.Id, false, true); ; //Set lại token
                }
                else
                {
                    await ReleaseTokenUse(_context, accessToken.Id, false, false); ; //Set lại token
                }

            }
            catch (Exception e)
            {
                throw new ExceptionCustom("CheckResponseAfterCallApi", request, e.Message, e);
            }
            return response;
        }
        #endregion
    }
}