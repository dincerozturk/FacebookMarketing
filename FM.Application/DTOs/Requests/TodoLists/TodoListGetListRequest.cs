using FM.Application.Wrappers.Requests;

namespace FM.Application.DTOs.Requests.TodoLists
{
    public class TodoListGetListRequest : PaginationFilterRequest
    {
        public string SearchValue { get; set; } = "";
    }
}