using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FM.Domain.Entities.Facebook
{
    public class FbPostGroupAction
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public long? PostGroupId { get; set; }

        [ForeignKey("PostGroupId")]
        public FbPostGroup PostGroup { get; set; }
    }
}
