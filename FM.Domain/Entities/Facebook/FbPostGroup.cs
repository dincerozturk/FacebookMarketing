using FM.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FM.Domain.Entities.Facebook
{
    public class FbPostGroup : AudiTableEntity<long>
    {
        [Key]
        public new long Id { get; set; }

        public DateTime? UpdatedTime { get; set; }

        public string Message { get; set; }

        public List<FbPostGroupAction> PostGroupActions { get; set; }

        public List<FbPostGroupComment> PostGroupComments { get; set; }

        public string Icon { get; set; }
        public string Link { get; set; }

        public bool IsHidden { get; set; }
        public bool IsExpired { get; set; }

        public string ObjectId { get; set; }

        public string Picture { get; set; }

        public FbPostGroupPrivacy PostGroupPrivacy { get; set; }

        public long ShareCount { get; set; }

        public string StatusType { get; set; }

        public bool Subscribed { get; set; }

        public string Type { get; set; }

        public string CreatedTime { get; set; }

        public string FbId { get; set; }
    }
}
