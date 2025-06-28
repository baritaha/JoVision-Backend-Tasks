namespace JoVisionBackend.Models
{
    public class FileUploadRequest
    {
        public IFormFile? Image { get; set; }
        public string? Owner { get; set; }
    }
}
