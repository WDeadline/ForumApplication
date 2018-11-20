using ForumApi.Interfaces;
using ForumApi.Interfaces.Repositories;
using ForumApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Services
{
    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter _imageWriter;
        private readonly IRepository<User> _userRepository;
        private readonly IHostingEnvironment _env;

        public ImageHandler(IImageWriter imageWriter, IRepository<User> userRepository, IHostingEnvironment env)
        {
            _imageWriter = imageWriter;
            _userRepository = userRepository;
            _env = env;
        }

        public async Task<string> UploadAvatar(string userId, IFormFile file)
        {
            var user = _userRepository.GetById(userId)?.Result;
            if(user == null)
            {
                return "";
            }
            _imageWriter.DeleteAvatar(user.Avatar);
            string result = await _imageWriter.UploadAvatar(file);
            user.Avatar = result.Replace("\\","/");
            _userRepository.Update(user);
            return result;
        }
    }
}
