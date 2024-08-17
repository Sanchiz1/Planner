using Application.Common.Models;
using Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Token GenerateToken(ApplicationUser applicationUser)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("Email", applicationUser.Email!),
            new Claim("DisplayName", applicationUser.DisplayName),
            new Claim("Id", applicationUser.Id.ToString()),
        };

        
        DateTime Issued = DateTime.UtcNow;
        DateTime Expires = Issued.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"]));

        var token = new JwtSecurityToken(
            _configuration["JwtSettings:Issuer"],
            _configuration["JwtSettings:Audience"],
            claims,
            notBefore: Issued,
            expires: Expires,
            signingCredentials: credentials
        );

        string value = new JwtSecurityTokenHandler().WriteToken(token);
        return new Token()
        {
            Value = value,
            Issued = Issued,
            Expires = Expires,
        };
    }
}
