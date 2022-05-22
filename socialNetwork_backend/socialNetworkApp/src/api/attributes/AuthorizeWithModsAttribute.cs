using Microsoft.AspNetCore.Authorization;
using socialNetworkApp.api.controllers.modifiersOfAccess;

namespace socialNetworkApp.api.controllers.auth;

/// <summary>
/// Specifies that the class or method that this attribute is applied to requires the specified authorization.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class AuthorizeWithModsAttribute : AuthorizeAttribute, IAuthorizeData
{
    protected EnumModList CurrentMods = new EnumModList() { };

    public AuthorizeWithModsAttribute(string policy)
    {
        Policy = policy;
    }

    public AuthorizeWithModsAttribute(AllModsEnum mod)
    {
        this.QuantityMod = mod;
    }

    public AuthorizeWithModsAttribute(params AllModsEnum[] mod)
    {
        foreach (var allModsEnum in mod)
        {
            this.QuantityMod = allModsEnum;
        }
    }

    public AuthorizeWithModsAttribute(EnumModList mods)
    {
        this.Mods = mods;
    }


    public EnumModList Mods
    {
        get { return CurrentMods; }
        set
        {
            CurrentMods += value;
            if (Roles == "" | Roles == null)
            {
                Roles = value.ToString();
            }
            else
            {
                Roles += "," + value.ToString();
            }
        }
    }

    public AllModsEnum QuantityMod
    {
        set
        {
            CurrentMods += value;
            if (Roles == "" | Roles == null)
            {
                Roles = value.ToString();
            }
            else
            {
                Roles += "," + value.ToString();
            }
        }
    }
}