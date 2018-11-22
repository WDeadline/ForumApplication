using ForumApi.Environments;
using ForumApi.Helpers;
using ForumApi.Interfaces;
using ForumApi.Interfaces.Services;
using ForumApi.Models;
using ForumApi.Payloads;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ForumApi.SourceCode.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IRepository<User> _userRepository;
        private readonly JwtTokenSettings jwtTokenSettings;
        private readonly IHostingEnvironment _env;
        public AuthenticationService(IOptions<JwtTokenSettings> options, ILogger<AuthenticationService> logger,
            IRepository<User> userRepository, IHostingEnvironment env)
        {
            jwtTokenSettings = options.Value;
            _logger = logger;
            _env = env;
            _userRepository = userRepository;
        }

        private string GenerateJSONWebToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSettings.SecretKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            double.TryParse(jwtTokenSettings.Duration, out double duration);
            DateTime expires = DateTime.UtcNow.AddMinutes(duration);
            List<Claim> claims = new List<Claim>(){
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            foreach (var role in user.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role + ""));

            var tokeOptions = new JwtSecurityToken(
                issuer: jwtTokenSettings.Issuer,
                audience: jwtTokenSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }

        public async Task<CurrentUser> AuthenticateAsync(string usernameOrEmailAddress, string password)
        {
            try
            {
                var user = await _userRepository.Get(u => u.Username == usernameOrEmailAddress ||
                                                        u.EmailAddress == usernameOrEmailAddress);

                if (user == null || !PasswordManager.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    return null;
                }

                var currentUser = new CurrentUser {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    EmailAddress = user.EmailAddress,
                    Avatar = user.Avatar,
                    Roles = user.Roles,
                    Token = GenerateJSONWebToken(user)
                };
                return currentUser;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, usernameOrEmailAddress);
                throw e;
            }
        }
    }
}
