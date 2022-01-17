using System;
using FM.Application.DTOs.Requests;
using FM.Application.Utilities;
using Microsoft.AspNetCore.WebUtilities;

namespace FM.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        //public Uri GetPostUri(string postId)
        //{
        //    return new Uri(_baseUri + ApiRoutes.Posts.Get.Replace("{postId}", postId));
        //}

        public Uri GetPageUri(PaginationFilter pagination, string route)
        {
            var _enpointUri = new Uri(UriUtility.Combine(_baseUri, route));
            if (pagination == null)
            {
                return _enpointUri;
            }
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", pagination.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
