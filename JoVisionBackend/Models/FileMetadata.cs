namespace JoVisionBackend.Models
{
    public class FileMetadata
    {
        public string FileName { get; set; }=string.Empty;
        public string Owner { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}
