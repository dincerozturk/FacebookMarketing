using System;
using FM.Application.DTOs.Requests;

namespace FM.Services
{
    public interface IUriService
    {
        //Uri GetPostUri(string postId);

        //Uri GetAllPostsUri(string route, PaginationFilter pagination = null);

        public Uri GetPageUri(PaginationFilter paginationFilter, string route);
    }
}
