using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FM.Domain.Common;
using FM.Domain.Entities.Identity;

namespace FM.Domain.Entities
{
    public class HealthHistory : AudiTableEntity<Guid>
    {
        public Guid? UserId { get; set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime RecordedDate { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public string ImageUrl { get; set; }
    }
}
