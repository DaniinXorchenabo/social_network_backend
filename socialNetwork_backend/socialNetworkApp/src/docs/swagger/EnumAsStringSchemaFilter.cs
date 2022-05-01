using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace socialNetworkApp.docs.swagger;

public class EnumAsStringSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema model, SchemaFilterContext context)
    {
        // context.Type.
        if (context.Type.IsEnum)
        {
            model.Enum.Clear();
            Enum.GetNames(context.Type)
                .ToList()
                .ForEach(name => model.Enum.Add(new OpenApiString($"{name}")));
        }
        // model.Sec
    }
}

public class TestDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument model, DocumentFilterContext context)
    {
        // model.SecurityRequirements.Select(x => x.Select(y => y.));
        // context.Type.
        // if (context.Type.IsEnum)
        // {
        //     model.Enum.Clear();
        //     Enum.GetNames(context.Type)
        //         .ToList()
        //         .ForEach(name => model.Enum.Add(new OpenApiString($"{name}")));
        // }
        // model.Sec
    }
}

public class TestOperationFilter : IOperationFilter
{
    // private readonly IHttpContextAccessor _HttpContextAccessor;
    //
    // public void AddAuthHeaderOperationFilter(IHttpContextAccessor httpContextAccessor)
    // {
    //     this._HttpContextAccessor = httpContextAccessor;
    // }
    // public void Apply(OpenApiOperation model, OperationFilterContext context)
    // {
    //     context.MethodInfo.Attributes.
    //     model.Security.
    //     // context.Type.
    //     // if (context.Type.IsEnum)
    //     // {
    //     //     model.Enum.Clear();
    //     //     Enum.GetNames(context.Type)
    //     //         .ToList()
    //     //         .ForEach(name => model.Enum.Add(new OpenApiString($"{name}")));
    //     // }
    //     // model.Sec
    // }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var actionMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;
        var isAuthorized = actionMetadata.Any(metadataItem => metadataItem is AuthorizeAttribute);
        var allowAnonymous = actionMetadata.Any(metadataItem => metadataItem is AllowAnonymousAttribute);


        if (!isAuthorized || allowAnonymous)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = "JWT access token",
                Required = true,
                // Type = SecuritySchemeType.ApiKey,
            });

            operation.Responses.Add("401", new OpenApiResponse() {Description = "Unauthorized"});
            operation.Responses.Add("403", new OpenApiResponse {Description = "Forbidden"});

            operation.Security = new List<OpenApiSecurityRequirement>();

            //Add JWT bearer type

            var securityItem = new OpenApiSecurityRequirement();
            // var securityScheme = new OpenApiSecurityScheme()
            // {
            //     Name = "Authorization",
            //     Type = SecuritySchemeType.ApiKey,
            //     Scheme = "Bearer",
            //     BearerFormat = "JWT",
            //     In = ParameterLocation.Header,
            //     Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
            //           Enter 'Bearer' [space] and then your token in the text input below.
            //           \r\n\r\nExample: 'Bearer 12345abcdef'",
            // };
            // securityItem[securityScheme] = new List<string>() {"Bearer"};
            
            operation.Security.Add(securityItem);
        }
    }
}

public class TestParameterFilter : IParameterFilter
{
    public void Apply(OpenApiParameter model, ParameterFilterContext context)
    {
        // context.Type.
        // if (context.Type.IsEnum)
        // {
        //     model.Enum.Clear();
        //     Enum.GetNames(context.Type)
        //         .ToList()
        //         .ForEach(name => model.Enum.Add(new OpenApiString($"{name}")));
        // }
        // model.Sec
    }
}

public class TestRequestBodyFilter : IRequestBodyFilter
{
    public void Apply(OpenApiRequestBody model, RequestBodyFilterContext context)
    {
        // context.Type.
        // if (context.Type.IsEnum)
        // {
        //     model.Enum.Clear();
        //     Enum.GetNames(context.Type)
        //         .ToList()
        //         .ForEach(name => model.Enum.Add(new OpenApiString($"{name}")));
        // }
        // model.Sec
    }
}