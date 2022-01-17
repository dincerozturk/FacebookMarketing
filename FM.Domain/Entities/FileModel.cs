using System;
using System.ComponentModel.DataAnnotations;

namespace FM.Domain.Entities
{
    public abstract class FileModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public string UploadedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    public class FileOnFileSystemModel : FileModel
    {
        public string FilePath { get; set; }
    }

    public class FileOnDatabaseModel : FileModel
    {
        public byte[] Data { get; set; }
    }
}
