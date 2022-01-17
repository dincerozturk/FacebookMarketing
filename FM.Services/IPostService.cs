using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FM.Application.DTOs.Requests;
using FM.Application.DTOs.Requests.Posts.Queries;
using FM.Domain.Entities;

namespace FM.Services
{
    public interface IPostService
    {
        Task<int> CountAllPostWithUser(GetAllPostsQuery query);
        Task<List<Post>> GetPostsAsync(GetAllPostsFilter filter = null, PaginationFilter paginationFilter = null);

        Task<bool> CreatePostAsync(Post post);

        Task<Post> GetPostByIdAsync(Guid postId);

        Task<bool> UpdatePostAsync(Post postToUpdate);

        Task<bool> DeletePostAsync(Guid postId);

        Task<bool> UserOwnsPostAsync(Guid postId, string userId);

        Task<List<Tag>> GetAllTagsAsync();

        Task<bool> CreateTagAsync(Tag tag);

        Task<Tag> GetTagByNameAsync(string tagName);

        Task<bool> DeleteTagAsync(string tagName);
    }
}
