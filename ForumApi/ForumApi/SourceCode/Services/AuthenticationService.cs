using ForumApi.Helpers;
using ForumApi.Models;
using ForumApi.Payloads;
using ForumApi.Repositories;
using ForumApi.Services;
using Microsoft.Extensions.Logging;
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
        private readonly IUserRepository _userRepository;
        
        public AuthenticationService(ILogger<AuthenticationService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        private string GenerateJSONWebToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TokenParameters:SecretKey"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>(){
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.DateOfBirth, user.CreationTime.ToLongDateString())
            };
            AddClaimRoles(user.Roles, claims);

            var tokeOptions = new JwtSecurityToken(
                issuer: "TokenParameters:Issuer",
                audience: "TokenParameters:Audience",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }

        private static void AddClaimRoles(IEnumerable<string> roles, List<Claim> claims)
        {
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        public async Task<UserDto> AuthenticateAsync(string usernameOrEmailAddress, string password)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameOrEmailAddressAsync(usernameOrEmailAddress);

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
                    FirstName = user.FirstName,
                    LastName = user.LastName,
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
