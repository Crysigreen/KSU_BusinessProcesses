using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace KSU_BProcesses.Controllers
{
    [Route("api")]
    [ApiController]
    public class UploadDownloadController : ControllerBase
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public UploadDownloadController(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            // var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            // var uploudss = Path.Combine("F:\\IntelliJ_projects\\KSU_Angular\\src\\assets\\", "estate_images");
            // //if (!Directory.Exists(uploudss))
            // //{
            // //    Directory.CreateDirectory(uploudss);
            // //}
            // //if (file.Length > 0)
            // //{
            //     var filePath = Path.Combine(uploudss, file.FileName);
            //     using (var fileStream = new FileStream(filePath, FileMode.Create))
            //     {
            //         await file.CopyToAsync(fileStream);
            //     }
            //// }
            // return Ok();

            try
            {
                if (file != null && file.Length > 0)
                {
                    // Генерируем уникальное имя файла для предотвращения перезаписи
                    var fileName = file.FileName;

                    // Получаем путь к файлу
                    var filePath = Path.Combine("F:\\IntelliJ_projects\\KSU_Angular\\src\\assets\\", "estate_images", fileName);
                    //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", fileName);

                    var directory = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Сохраняем файл на сервере
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return Ok(new { fileName });
                }
                else
                {
                    return BadRequest("File is empty");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> Download([FromQuery] string file)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            var uploudss = Path.Combine("F:\\IntelliJ_projects\\KSU_Angular\\src\\assets\\", "estate_images");
            var filePath = Path.Combine(uploudss, file);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), file);
        }

        [HttpGet]
        [Route("files")]
        public IActionResult Files()
        {
            var result = new List<string>();

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            var uploudss = Path.Combine("F:\\IntelliJ_projects\\KSU_Angular\\src\\assets\\", "estate_images");
            if (Directory.Exists(uploudss))
            {
                var provider = _hostingEnvironment.ContentRootFileProvider;
                foreach (string fileName in Directory.GetFiles(uploudss))
                {
                    var fileInfo = provider.GetFileInfo(fileName);
                    result.Add(fileInfo.Name);
                }
            }
            return Ok(result);
        }


        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
