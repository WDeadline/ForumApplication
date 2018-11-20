using ForumApi.Environments;
using ForumApi.Helpers;
using ForumApi.Interfaces;
using ForumApi.Interfaces.Repositories;
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

namespace ForumApi.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly JwtTokenSettings jwtTokenSettings;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IOptions<JwtTokenSettings> options,
            IUserRepository userRepository)
        {
            _logger = logger;
            jwtTokenSettings = options.Value;
            _userRepository = userRepository;

        }

        public Task Add(User entity)
        {
            try
            {
                return _userRepository.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, entity);
                throw ex;
            }
        }

        public Task<bool> Delete(string id)
        {
            try
            {
                return _userRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return _userRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public Task<User> GetById(string id)
        {
            try
            {
                return _userRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<UserDto> Login(string usernameOrEmailAddress, string password)
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

                var dto = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    EmailAddress = user.EmailAddress,
                    Avatar = user.Avatar,
                    Roles = user.Roles,
                    Token = GenerateJSONWebToken(user)
                };
                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, usernameOrEmailAddress);
                throw ex;
            }
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

        public async Task<bool> IsExistedEmailAddress(string emailAddress)
        {
            var user = await _userRepository.GetUserByEmailAddressAsync(emailAddress);
            return (user != null);
        }

        public async Task<bool> IsExistedUsername(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            return (user != null);
        }

        public async Task<bool> Register(Register register)
        {
            try
            {
                PasswordManager.CreatePasswordHash(register.Password, out byte[] passwordHash, out byte[] passwordSalt);
                User user = new User
                {
                    Username = register.Username,
                    EmailAddress = register.EmailAddress,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Roles = new List<Role> { Role.Student }
                };
                await _userRepository.Add(user);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, register);
                throw e;
            }
        }

        public Task<bool> Update(User entity)
        {
            try
            {
                return _userRepository.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, entity);
                throw ex;
            }
        }
    }
}
