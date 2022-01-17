using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FM.Domain.Entities.Facebook
{
    public class FbPostGroupPrivacy
    {
        [Key]
        public long Id { get; set; }

        public string Allow { get; set; }

        public string Deny { get; set; }

        public string Description { get; set; }

        public string Friends { get; set; }

        public string Value { get; set; }

        public long? PostGroupId { get; set; }

        [ForeignKey("PostGroupId")]
        public FbPostGroup PostGroup { get; set; }
    }
}