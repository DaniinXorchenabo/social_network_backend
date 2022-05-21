﻿using System.IdentityModel.Tokens.Jwt;
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
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using socialNetworkApp.api.controllers.auth;
using socialNetworkApp.api.controllers.modifiersOfAccess;
using socialNetworkApp.api.responses;

namespace socialNetworkApp.api.controllers.users;

[Tags("user")]
[ApiController]
[Route("user")]
[Produces("application/json")]
public class UserController : Controller
{
    protected readonly IEnv Env_;
    protected readonly BaseBdConnection Db;

    public UserController(IEnv env, BaseBdConnection context)
    {
        Env_ = env;
        Db = context;
    }

    [HttpPost("new")]
    [ValidationActionFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<Resp> CreateUser(CreateUser newUser)
    {
        // ModelState.IsValid
        await using (var db = Db)
        {
            var newUserDb = new UserDb(newUser)
            {
                Id = Guid.NewGuid(),
                HashedPassword = Security.GetHashedPassword(newUser.Password),
                Mods = new List<AllModsEnum>(RolesFromMods.User) 
            };
            Console.WriteLine(newUser);
            Console.WriteLine(newUserDb);
            await db.Users.AddAsync(newUserDb);
            await db.SaveChangesAsync();
        }

        return new Resp(200, new ObjectResult(newUser));
    }
}

public static class AuthOptions
{
    public static SymmetricSecurityKey GetSymmetricSecurityKey(string key) =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
}