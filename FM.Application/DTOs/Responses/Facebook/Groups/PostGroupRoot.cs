using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FM.Application.DTOs.Responses.Facebook.Groups
{

    public class PostGroupRoot
    {
        [JsonProperty("data")]
        public List<PostGroupDto> Data { get; set; }

        [JsonProperty("paging")]
        public PostGroupRootPaging Paging { get; set; }
    }

    public class PostGroupDto
    {
        [JsonProperty("updated_time")]
        public DateTime UpdatedTime { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("actions")]
        public List<PostGroupActionDto> Actions { get; set; }

        [JsonProperty("comments")]
        public PostGroupCommentsDto PostGroupCommentsDto { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("is_expired")]
        public bool IsExpired { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("message_tags")]
        public PostGroupMessageTagsDto PostGroupMessageTagsDto { get; set; }

        [JsonProperty("object_id")]
        public string ObjectId { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("privacy")]
        public PostGroupPrivacyDto PostGroupPrivacyDto { get; set; }

        [JsonProperty("shares")]
        public PostGroupSharesDto PostGroupSharesDto { get; set; }

        [JsonProperty("status_type")]
        public string StatusType { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("id")]
        public string FbId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public class PostGroupActionDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }
    }

    public class PostGroupCommentsDto
    {
        [JsonProperty("data")]
        public List<PostGroupCommentDto> Data { get; set; }

        [JsonProperty("paging")]
        public PostGroupCommentsPaging Paging { get; set; }
    }

    public class PostGroupCommentDto
    {
        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("can_remove")]
        public bool CanRemove { get; set; }

        [JsonProperty("like_count")]
        public long LikeCount { get; set; }

        [JsonProperty("user_likes")]
        public bool UserLikes { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class PostGroupCommentsPaging
    {
        [JsonProperty("cursors")]
        public Cursors Cursors { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }

    public class Cursors
    {
        [JsonProperty("before")]
        public string Before { get; set; }

        [JsonProperty("after")]
        public string After { get; set; }
    }

    public class PostGroupMessageTagsDto
    {
    }

    public class PostGroupPrivacyDto
    {
        [JsonProperty("allow")]
        public string Allow { get; set; }

        [JsonProperty("deny")]
        public string Deny { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("friends")]
        public string Friends { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class PostGroupSharesDto
    {
        [JsonProperty("count")]
        public long Count { get; set; }
    }

    public class PostGroupRootPaging
    {
        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }
}
