using AutoMapper;
using ForumApi.Authentications;
using ForumApi.Helpers;
using ForumApi.Models;
using ForumApi.Payloads;
using ForumApi.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ForumApi.SourceCode.Authentications
{
    public class AuthenticationService : IAuthentication
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthenticationService(ILogger logger, IUserRepository userRepository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;

        }

        private string GenerateJSONWebToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TokenParameters:SecretKey"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble("TokenParameters:Expires"));

            //tecnical debt: add role
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim(ClaimTypes.Role, "Manager"),
                    new Claim(ClaimTypes.Role, "Manager"),

                };

            var tokeOptions = new JwtSecurityToken(
                issuer: "TokenParameters:Issuer",
                audience: "TokenParameters:Audience",
                claims: claims,
                expires: expires,
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }

        public async Task<UserDto> AuthenticateAsync(string usernameOrEmailAddress, string password)
        {
            if (string.IsNullOrEmpty(usernameOrEmailAddress) || string.IsNullOrEmpty(password))
                return null;

            var user = await _userRepository.GetUserByUsernameOrEmailAddress(usernameOrEmailAddress);

            if (user == null)
                return null;

            if (!PasswordManager.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = GenerateJSONWebToken(user);
            return userDto;
        }
    }
}
