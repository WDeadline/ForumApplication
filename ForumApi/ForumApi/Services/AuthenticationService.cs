using ForumApi.Environments;
using ForumApi.Helpers;
using ForumApi.Interfaces.Repositories;
using ForumApi.Interfaces.Services;
using ForumApi.Models;
using ForumApi.Payloads;
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
        public AuthenticationService(IOptions<JwtTokenSettings> options, ILogger<AuthenticationService> logger,
            IRepository<User> userRepository)
        {
            jwtTokenSettings = options.Value;
            _logger = logger;
            _userRepository = userRepository;
        }

        private string GenerateJSONWebToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSettings.SecretKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            double.TryParse(jwtTokenSettings.Duration, out double duration);
            DateTime expires = DateTime.UtcNow.AddMinutes(duration);
            List<Claim> claims = new List<Claim>(){
                new Claim(ClaimTypes.Name, user.Username)
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

        public async Task<UserDto> AuthenticateAsync(string usernameOrEmailAddress, string password)
        {
            try
            {
                var user = await _userRepository
                    .Get(u => u.Username == usernameOrEmailAddress || u.EmailAddress == usernameOrEmailAddress);

                if (user == null)
                {
                    return null;
                }

                if (!PasswordManager.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    return null;
                }

                var dto = new UserDto {
                    Id = user.Id,
                    Username = user.Username,
                    EmailAddress = user.EmailAddress,
                    Roles = user.Roles,
                    Token = GenerateJSONWebToken(user)
                };
                return dto;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, usernameOrEmailAddress);
                throw e;
            }
        }
    }
}
