using ForumApi.Models;
using ForumApi.Payloads;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ForumApi.Authentications
{
    public class JwtTokenProvider
    {
        private string GenerateJSONWebToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TokenParameters:SecretKey"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble("TokenParameters:Expires"));
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, "Manager"),
                    new Claim(ClaimTypes.Role, "Manager"),

                };

            var tokeOptions = new JwtSecurityToken(
                issuer: "TokenParameters:Issuer",
                audience: "TokenParameters:Audience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );
            //technical debt
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }

        private User AuthenticateUser(Login login)
        {
            // return one user
            return null;
        }
    }
}
