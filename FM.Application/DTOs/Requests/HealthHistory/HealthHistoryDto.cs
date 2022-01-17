using System;

namespace FM.Application.DTOs.Requests.HealthHistory
{
    public class HealthHistoryDto
    {
        public DateTime RecordedDate { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public string ImageUrl { get; set; }
    }
}
