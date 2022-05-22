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
public  class UserDto : AbstractDto
{
    public Guid Id{ get; set; }
    public string Username{ get; set; }
    public string HashedPassword{ get; set; }
    
    public UserDto(object obj) : base(obj){}
    
    public UserDto(){}
}


// public interface EmailField
// {
//     
//
// }
[AddAnswerType(AnswerType.UserAnswer)]
public class UserDtoAbstract : AbstractDto //, EmailField
{
    [Required][Display(Name="username")]  public virtual string Username { get; set; } = default!;
    [Required][Display(Name="name")] public virtual string Name { get; set; } = default!;
    [Required][Display(Name="surname")] public virtual string Surname { get; set; } = default!;
    [Required][EmailAddress] [Display(Name="email")] public string Email { get; set; }
    
    public UserDtoAbstract(object obj) : base(obj){}
    
    public UserDtoAbstract(){}
}


[AddAnswerType(AnswerType.UserAnswer)]
public class CreateUser: UserDtoAbstract
{
    [Required][Display(Name="password")] public string Password { get; set; }
    // [JsonIgnore][Display(Name="hashed_password")] public string HashedPassword { get; set; }
    public CreateUser(object obj) : base(obj){}
    
    public CreateUser(){}
}

[AddAnswerType(AnswerType.UserAnswer)]
public class GetUser : UserDtoAbstract
{
    [Required][Display(Name="id")] public virtual Guid Id { get; set; } = default!;
    public GetUser(object obj) : base(obj){}
    
    public GetUser(){}
}