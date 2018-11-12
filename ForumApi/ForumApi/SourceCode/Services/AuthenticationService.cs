using AutoMapper;
using ForumApi.Environments;
using ForumApi.Helpers;
using ForumApi.Models;
using ForumApi.Payloads;
using ForumApi.Repositories;
using ForumApi.Services;
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
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        

        public AuthenticationService(ILogger<AuthenticationService> logger, IMapper mapper, IUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        private string GenerateJSONWebToken(User user)
        {
            var claims = new List<Claim>();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TokenParameters:SecretKey"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            AddClaimRoles(user, claims);

            var tokeOptions = new JwtSecurityToken(
                issuer: "TokenParameters:Issuer",
                audience: "TokenParameters:Audience",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }

        private static void AddClaimRoles(User user, List<Claim> claims)
        {
            foreach (string role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        public async Task<UserDto> AuthenticateAsync(string usernameOrEmailAddress, string password)
        {
            if (string.IsNullOrEmpty(usernameOrEmailAddress) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = await _userRepository.GetUserByUsernameOrEmailAddress(usernameOrEmailAddress);

            if (user == null)
            {
                return null;
            } 

            if (!PasswordManager.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = GenerateJSONWebToken(user);
            return userDto;
        }
    }
}
