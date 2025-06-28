using JoVisionBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace JoVisionBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileTask47Controller : ControllerBase
    {
        private readonly string _imageDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        private readonly string _metaDir = Path.Combine(Directory.GetCurrentDirectory(), "Metadata");
        public FileTask47Controller()
        {
            Directory.CreateDirectory(_imageDir);
            Directory.CreateDirectory(_metaDir);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateFile([FromForm] FileUploadRequest request)
        {
            if (request.Image == null || string.IsNullOrWhiteSpace(request.Owner))
                return BadRequest("Image and owner are required.");

            if (!request.Image.ContentType.Contains("image/jpeg"))
                return BadRequest("Only JPEG format is allowed.");

            var fileName = Path.GetFileName(request.Image.FileName);
            var imagePath = Path.Combine(_imageDir, fileName);
            var metaPath = Path.Combine(_metaDir, fileName + ".json");

            if (System.IO.File.Exists(imagePath))
                return BadRequest("File already exists.");

            try
            {
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                }

                var metadata = new FileMetadata
                {
                    FileName = fileName,
                    Owner = request.Owner!,
                    CreatedAt = DateTime.Now,
                    LastModifiedAt = DateTime.Now
                };

                var json = JsonSerializer.Serialize(metadata, new JsonSerializerOptions { WriteIndented = true });
                await System.IO.File.WriteAllTextAsync(metaPath, json);

                return Created("", $"File '{fileName}' uploaded successfully.");
            }
            catch
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("delete")]
        public IActionResult DeleteFile([FromQuery] string fileName, [FromQuery] string fileOwner)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(fileOwner))
                return BadRequest("Missing query parameters.");

            var imagePath = Path.Combine(_imageDir, fileName);
            var metaPath = Path.Combine(_metaDir, fileName + ".json");

            if (!System.IO.File.Exists(imagePath) || !System.IO.File.Exists(metaPath))
                return BadRequest("File or metadata not found.");

            try
            {
                var json = System.IO.File.ReadAllText(metaPath);
                var metadata = JsonSerializer.Deserialize<FileMetadata>(json);

                if (metadata == null || metadata.Owner != fileOwner)
                    return StatusCode(403, "Forbidden: Owner mismatch.");

                System.IO.File.Delete(imagePath);
                System.IO.File.Delete(metaPath);

                return Ok($"File '{fileName}' deleted.");
            }
            catch
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
