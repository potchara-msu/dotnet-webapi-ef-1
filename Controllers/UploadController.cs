using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_webapi_ef.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public UploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            var filePath = Path.Combine(_env.ContentRootPath, "uploads", fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, MimeTypes.GetMimeType(fileName)); // Adjust content type as needed
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            // Get the upload directory path
            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            // Create the uploads directory if it doesn't exist
            if (Directory.Exists(uploadDir) == false)
            {
                Directory.CreateDirectory(uploadDir);
            }

            // Get the full file path
            var uniqueName = FileHelper.GenerateUniqueFilename(imageFile.FileName);
            var filePath = Path.Combine(uploadDir, uniqueName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            var response = new
            {
                originalName = imageFile.FileName,
                uploadedName = uniqueName,
                mimeTypes = imageFile.ContentType
            };
            return Ok(response);
        }
    }
    class FileHelper
    {
        public static string GenerateUniqueFilename(string originalFilename)
        {
            var extension = Path.GetExtension(originalFilename);
            var filename = Path.GetFileNameWithoutExtension(originalFilename);

            var uniquePart = $"{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}";
            var newFilename = $"{filename}_{uniquePart}{extension}";

            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            var filePath = Path.Combine(uploadDir, newFilename);

            // Check for file existence and handle collisions
            int counter = 1;
            while (System.IO.File.Exists(filePath))
            {
                newFilename = $"{filename}_{uniquePart}_{counter}{extension}";
                filePath = Path.Combine(uploadDir, newFilename);
                counter++;
            }

            return newFilename;
        }
    }
}