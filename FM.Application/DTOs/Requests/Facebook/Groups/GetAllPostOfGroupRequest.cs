namespace FM.Application.DTOs.Requests.Facebook.Groups
{
    public record GetAllPostOfGroupRequest
    {
        public string GroupId { get; set; }

        public int Limit { get; set; }

        public string Since { get; set; }

        public string Until { get; set; }

        public string AccessToken { get; set; }

        public string PagingToken { get; set; }

        public string UrlGet =>
          $"https://graph.facebook.com/{GroupId}/feed?limit={Limit}&since={Since}&until={Until}&access_token={AccessToken}&__paging_token={PagingToken}&__previous";
    }
}