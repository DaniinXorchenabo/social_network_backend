using Microsoft.AspNetCore.Authorization;

namespace socialNetworkApp.api.controllers.modifiersOfAccess;


 
class AgeRequirement : IAuthorizationRequirement
{
    protected internal int Age { get; set; }
    public AgeRequirement(int age) => Age = age;
}