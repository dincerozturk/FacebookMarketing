using FM.Application.Interfaces.Shared;
using FM.Application.Utilities;
using FM.Application.Wrappers.Requests;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace FM.Infrastructure.Shared.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPageUri(PaginationFilterRequest pagination, string route)
        {
            var endpointUri = new Uri(UriUtility.Combine(_baseUri, route));

            if (pagination == null)
            {
                return endpointUri;
            }

            var modifiedUri = QueryHelpers.AddQueryString(endpointUri.ToString(), "pageNumber", pagination.PageIndex.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());

            return new Uri(modifiedUri);
        }
    }
}