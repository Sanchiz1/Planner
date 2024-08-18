using Application.Common.Claims;
using Application.Common.Models;
using Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            new Claim(AppClaims.EmailClaimType, applicationUser.Email!),
            new Claim(AppClaims.DisplayNameClaimType, applicationUser.DisplayName),
            new Claim(AppClaims.IdClaimType, applicationUser.Id.ToString()),
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
