using System;

namespace FM.Application.DTOs.Requests.HealthHistory
{
    public class HealthHistoryGetDto : HealthHistoryDto
    {
        public Guid Id { get; set; }
    }
}
