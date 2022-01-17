using System;

namespace FM.Application.DTOs.Responses.TodoLists
{
    public class TodoListsDtoResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedByName { get; set; }
    }
}