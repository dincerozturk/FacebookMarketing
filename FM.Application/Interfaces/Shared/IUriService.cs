using System;
using FM.Application.Wrappers.Requests;

namespace FM.Application.Interfaces.Shared
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilterRequest paginationFilter, string route);
    }
}