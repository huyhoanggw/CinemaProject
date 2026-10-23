using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Features.Services.Helpers
{
    public static class FileUpLoad
    {
        public static async Task<string> Upload(IFormFile file)
        {
            var FolderUpload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(FolderUpload))
            {
                Directory.CreateDirectory(FolderUpload);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fileCombine = Path.Combine(FolderUpload, fileName);
            await using var stream = new FileStream(fileCombine, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();
            return fileName;
        }

    }
}
