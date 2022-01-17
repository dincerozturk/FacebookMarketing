using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FM.Domain.Entities.Facebook
{
    public class FbPostGroupComment
    {
        [Key]
        public long Id { get; set; }
      
        public string CreatedTime { get; set; }
        
        public string Message { get; set; }
       
        public bool CanRemove { get; set; }
       
        public long LikeCount { get; set; }
       
        public long UserLikes { get; set; }
        
        public string FbId { get; set; }
       
        public long? PostGroupId { get; set; }

        [ForeignKey("PostGroupId")]
        public FbPostGroup PostGroup { get; set; }
    }
}