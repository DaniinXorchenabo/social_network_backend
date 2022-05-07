using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using socialNetworkApp.api.dtos;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.utils;

namespace socialNetworkApp.api.controllers.users;

[AddAnswerType(AnswerType.UserAnswer)]
public record class UserDto(
    Guid Id,
    string Username,
    string HashedPassword
) : EmptyAnswer;


public class UserDtoAbstract : AbstractDto
{
    private static HashSet<string> _MyProperties { get; set; }

    public virtual HashSet<string> MyProperties
    {
        get => UserDtoAbstract._MyProperties;
    }

    static UserDtoAbstract()
    {
        var classType = typeof(UserDtoAbstract);
        _MyProperties = new HashSet<string>();
        _ = classType.GetProperties()
            .Where(x => x.PropertyType.IsPublic)
            .Select(x => _MyProperties.Add(x.Name));
    }
    
    
    [Required] [EmailAddress] [Display(Name="email")] public virtual string Email { get; set; } = default!;
    [Required][Display(Name="username")]  public virtual string Username { get; set; } = default!;
    [Required][Display(Name="name")] public virtual string Name { get; set; } = default!;
    [Required][Display(Name="surname")] public virtual string Surname { get; set; } = default!;
}


public class CreateUser: UserDtoAbstract
{
    private static HashSet<string> _MyProperties { get; set; }

    public virtual HashSet<string> MyProperties
    {
        get => CreateUser._MyProperties;
    }

    static CreateUser()
    {
        var classType = typeof(CreateUser);
        _MyProperties = new HashSet<string>();
        _ = classType.GetProperties()
            .Where(x => x.PropertyType.IsPublic)
            .Select(x => _MyProperties.Add(x.Name));
    }
    
    [Required][Display(Name="password")] public string Password { get; set; }
    // [JsonIgnore][Display(Name="hashed_password")] public string HashedPassword { get; set; }
}