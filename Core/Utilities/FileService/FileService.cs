﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.FileService
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var filename = $"{Guid.NewGuid()}_{file.FileName}";
            string path = Path.Combine(_environment.WebRootPath, "assets/images", filename);
            using (FileStream fileStream = new(path, FileMode.Create, FileAccess.ReadWrite))
            {
                await file.CopyToAsync(fileStream);
            }
            return filename;
        }

        public void Delete(string filename)
        {
            string path = Path.Combine(_environment.WebRootPath, "assets/images", filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public bool CheckPhoto(IFormFile file)
        {
            if (file.ContentType.Contains("image/"))
            {
                return true;
            }
            return false;
        }

        public bool MaxSize(IFormFile file, int maxSize)
        {
            if (file.Length / 1024 > maxSize)
            {
                return false;
            }
            return true;
        }

        public string ReadFile(string path, string template)
        {

            using (StreamReader reader = new StreamReader(path))
            {
                template = reader.ReadToEnd();
            }
            return template;
        }
    }
}
