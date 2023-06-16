using System.IdentityModel.Tokens.Jwt;
using System;
using System.Linq;
using AcerPro.Persistence.DTOs;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace AcerPro.Presentation.Server.Infrastructures;

public static class JwtUtility
{
    public static Tuple<string, DateTime> GenerateJwtToken
        (string email,int userId, string secretKey)
    {
        // Generate token that is valid for 5 hours

        var key =
            Encoding.ASCII.GetBytes(secretKey);

        var symmetricSecurityKey =
            new SymmetricSecurityKey(key: key);

        var securityAlgorithm =
            SecurityAlgorithms.HmacSha256Signature;

        var signingCredentials =
            new SigningCredentials(key: symmetricSecurityKey, algorithm: securityAlgorithm);

        var tokenDescriptor =
            new SecurityTokenDescriptor
            {
                Subject =
                    new System.Security.Claims.ClaimsIdentity
                    (new[]
                    {
                        new System.Security.Claims.Claim
                            (type: System.Security.Claims.ClaimTypes.Name, value: email),
                        new System.Security.Claims.Claim
                            ("Id", value: userId.ToString()),
                    }),
                Expires =
                    DateTime.UtcNow.AddHours(5),

                SigningCredentials = signingCredentials,
            };

        var tokenHandler =
            new JwtSecurityTokenHandler();

        var token =
            tokenHandler.CreateToken(tokenDescriptor: tokenDescriptor);

        string tokenString =
            tokenHandler.WriteToken(token: token);

        return new Tuple<string, DateTime>(tokenString, tokenDescriptor.Expires.Value);
    }

    public static void AttachUserToContext
        (HttpContext context, string token, string secretKey)
    {
        try
        {
            var tokenHandler =
                new JwtSecurityTokenHandler();

            var key =
                Encoding.ASCII.GetBytes(secretKey);

            tokenHandler.ValidateToken(token: token,
                validationParameters: new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey =
                        new SymmetricSecurityKey(key),

                    ClockSkew =
                        TimeSpan.Zero
                }, out SecurityToken validatedToken);

            var jwtToken =
                validatedToken as JwtSecurityToken;

            System.Security.Claims.Claim usernameClaim =
                jwtToken.Claims
                .Where(current => current.Type.ToLower() == "unique_name".ToLower())
                .FirstOrDefault();

            if (usernameClaim == null)
            {
                return;
            }

            System.Security.Claims.Claim userIdClaim =
               jwtToken.Claims
               .Where(current => current.Type.ToLower() == "Id".ToLower())
               .FirstOrDefault();

            // Attach user to context on successful jwt validation
            context.Items["User"] = new UserDto
            {
                Id = int.Parse(userIdClaim.Value),
                Email = usernameClaim.Value,
            };

        }
        catch 
        {
        }
    }
}
