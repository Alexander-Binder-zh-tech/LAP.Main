using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;

namespace LAP.Security.Models;

[Table("lap_blacklistedToken")]
public class BlacklistedToken
{
    public BlacklistedToken(string token)
    {
        Token = token;
        var handler = new JwtSecurityTokenHandler();
        var decoded = handler.ReadJwtToken(token);
        Expire = decoded.ValidTo;
    }

    public long Id { get; set; }
    public string Token { get; init; }
    public DateTime Expire { get; set; }
}