using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Interfaces
{
    public interface IImageWriter
    {
        Task<string> UploadAvatar(IFormFile file);
        void DeleteAvatar(string fileName);
    }
}
