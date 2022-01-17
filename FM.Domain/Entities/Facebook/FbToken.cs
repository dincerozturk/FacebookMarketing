using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FM.Domain.Entities.Facebook
{
    public class FbToken
    {
        [Key]
        public long Id { get; set; }

        public string Uid { get; set; }

        public string Name { get; set; }

        public string Cookie { get; set; }

        public string Token { get; set; }

        public bool IsDead { get; set; }

        public bool IsUsing { get; set; }

        public DateTime LastCalledTime { get; set; }

        public int TotalCalled { get; set; }

        public int TotalCalledInLastHour { get; set; }

        public int TotalCalledInTimeFrame { get; set; }

        public int MaxRequestInTimeFrame { get; set; }

        public int TimeFrameMinute { get; set; }

        public string Note { get; set; }

        public long? FbAccountId { get; set; }

        [ForeignKey("FbAccountId")]
        public FbAccount FbAccount { get; set; }
    }
}