using System.IdentityModel.Tokens.Jwt;

namespace GourmetGo.Desktop.Helpers;

public static class TokenHelper
{
    public static int GetUsuarioIdFromToken(string jwt)
    {
        var token = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
        var sub = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        return int.TryParse(sub, out var id) ? id : 0;
    }

    public static string GetNombreFromToken(string jwt)
    {
        var token = new JwtSecurityTokenHandler().ReadJwtToken(jwt);

        return token.Claims.FirstOrDefault(c => c.Type == "unique_name" || c.Type == "name")?.Value
               ?? token.Claims.FirstOrDefault(c => c.Type.EndsWith("/name"))?.Value
               ?? string.Empty;
    }

    public static string GetRolFromToken(string jwt)
    {
        var token = new JwtSecurityTokenHandler().ReadJwtToken(jwt);

        return token.Claims.FirstOrDefault(c => c.Type == "role")?.Value
               ?? token.Claims.FirstOrDefault(c => c.Type.EndsWith("/role"))?.Value
               ?? string.Empty;
    }
}