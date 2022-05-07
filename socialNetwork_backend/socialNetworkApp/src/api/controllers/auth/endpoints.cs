using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using socialNetworkApp.api.controllers.messages;
using socialNetworkApp.config;
using socialNetworkApp.db;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using socialNetworkApp.api.controllers.modifiersOfAccess;
using socialNetworkApp.api.controllers.users;

namespace socialNetworkApp.api.controllers.auth;

[ApiController]
[Route("auth")]
[Produces("application/json")]
public class AuthController : Controller
{
    protected readonly IEnv Env_;
    protected readonly BaseBdConnection Db;

    public AuthController(IEnv env, BaseBdConnection context)
    {
        Env_ = env;
        Db = context;
    }

    [HttpPost("token")]
    [ProducesResponseType(typeof(TokenAnswer), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    public async Task< IActionResult> GetAuthToken(
        [FromForm] string username, // check it: https://metanit.com/sharp/aspnet5/8.5.php
        [FromForm] string password,
        [FromForm] AllModsEnum[]? scopes = null
    )
    {
        if (scopes == null)
        {
            scopes = new AllModsEnum[]{};
        }
        UserDb? user;
        await using (var db = Db)
        {
            user = await db.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null)
            {
                return new UnauthorizedResult();
                
            }
        }

        if (!Security.Verify(password, user.HashedPassword))
        {
            return new UnauthorizedResult();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            // new Claim(ClaimTypes.Role, new EnumModList(scopes).ToString()),
        };
        var scopesAsSet = new EnumModList(scopes);
        scopesAsSet.IntersectWith(new EnumModList(user.Mods.ToArray()));
        foreach (var scope in scopesAsSet)
        {
            claims.Add(new Claim(ClaimTypes.Role, scope.ToString()));
        }
        
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: Env_.Backend.Address,
           
            audience: "audience", // TODO: add audience
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(
                AuthOptions.GetSymmetricSecurityKey(Env_.Backend.SecretKey),
                SecurityAlgorithms.HmacSha256)
        );

        return new ObjectResult(new TokenAnswer(
            TokenType.bearer,
            new JwtSecurityTokenHandler().WriteToken(jwt)));
    }
}

public static class AuthOptions
{
    public static SymmetricSecurityKey GetSymmetricSecurityKey(string key) =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
}