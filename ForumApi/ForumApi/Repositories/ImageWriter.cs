using ForumApi.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Interfaces
{
    public class ImageWriter : IImageWriter
    {
        public async Task<string> UploadAvatar(IFormFile file)
        {
            if (CheckIfImageFile(file))
            {
                return await WriteFile(file);
            }

            return "Invalid image file";
        }

        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
        }

        public async Task<string> WriteFile(IFormFile file)
        {
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string fileName = Guid.NewGuid().ToString() + extension;
                string folder = Path.Combine("images", "avatars");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, fileName);
                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
                return Path.Combine(folder, fileName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeleteAvatar(string fileName)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
