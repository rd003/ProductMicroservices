using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Models;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Services;

public class JwtTokenService
{
    private readonly List<User> _users =
    [
        new("admin", "aDm1n", "Administrator", ["read","write"]),
        new("user01", "u$3r01", "User", ["read"])
    ];
    private readonly IConfiguration _config;

    public JwtTokenService(IConfiguration config)
    {
        _config = config;
    }

    public AuthenticationToken? GenerateAuthToken(LoginModel loginModel)
    {
        var user = _users.FirstOrDefault(u => u.Username == loginModel.Username
                                                  && u.Password == loginModel.Password);
        if (user is null)
        {
            return null;
        }
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var expirationTimeStamp = DateTime.Now.AddMinutes(2);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Name, user.Username),
            new("role", user.Role),
            new("scope", string.Join(" ", user.Scopes))
        };

        var tokenOptions = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            claims: claims,
            expires: expirationTimeStamp,
            signingCredentials: signingCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new AuthenticationToken(tokenString, (int)expirationTimeStamp.Subtract(DateTime.Now).TotalSeconds);
    }
}

