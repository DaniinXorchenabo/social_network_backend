using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using socialNetworkApp.api.attributes;
using socialNetworkApp.api.responses.error;
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
        
        if (context.Type == typeof(OneFieldErrorValidate))
        {
            Console.WriteLine("");
        }
        
        if (context.Type == typeof(ValidateOneField))
        {
            Console.WriteLine("");
        }
        
        if (context.Type.IsGenericType && context.Type.GetGenericTypeDefinition() == typeof(ValidateError<>))
        {
            Console.WriteLine("");
            // model.Example = new OpenApiObject();
            // model.Example[context]
        }
        
        if (context.Type.IsGenericType && context.Type.GetGenericTypeDefinition().IsSubclassOf(typeof(ValidateError<>)))
        {
            Console.WriteLine("");
            // model.Example = new OpenApiObject();
            // model.Example[context]
        }
        
        if (context.Type.BaseType != null && context.Type.BaseType.IsGenericType &&
            context.Type.BaseType.GetGenericTypeDefinition() == typeof(ValidateError<>))
        {
            Console.WriteLine("");
            model.Example = new OpenApiObject();
            var dictEnumField = context.Type.GetProperties()
                .Where(y => y
                    .CustomAttributes
                    .Any(x => x.AttributeType == typeof(DictEnumSwaggerAttribute)))
                .FirstOrDefault();
            if (dictEnumField != null)
            {
                Dictionary<string, OpenApiSchema> dd = new Dictionary<string, OpenApiSchema>();
        
                Enum.GetNames(context.Type.BaseType.GenericTypeArguments.Where(x => x.IsSubclassOf(typeof(Enum)))
                        .FirstOrDefault()).ToList()
                    .ForEach(name =>
                    {
                        dd[name] = context.SchemaRepository.Schemas
                            .FirstOrDefault(
                                x => x.Key == dictEnumField
                                    .PropertyType
                                    .GenericTypeArguments[1]
                                    .Name).Value;
                    });
                // Enum.GetNames(context.Type.BaseType.GenericTypeArguments.Where(x => x.IsSubclassOf(typeof(Enum)))
                //     .FirstOrDefault()).ToList().ForEach(
                //     name =>
                //     {
                //         
                //     }
                // );
                    
                // context.SchemaGenerator.GenerateSchema(
                //     context.Type.BaseType.GenericTypeArguments.Where(x => x.IsSubclassOf(typeof(Enum)))
                //         .FirstOrDefault(), new SchemaRepository("dfdfdf")
                // );
                //
                // var o = new OpenApiObject();
                // foreach (var openApiSchema in dd)
                // {
                //     model.Example.Write(
                //         new OpenApiJsonWriter(
                //             new StringWriter(
                //                 new StringBuilder($"{openApiSchema.Key}: dfdf"),
                //                 null
                //             )
                //         )
                //     );
                // }
            }
        }
        
        if (context.Type == typeof(Dictionary<string, ValidateOneField>))
        {
            Console.WriteLine("");
        }
        
        if (context.Type == typeof(Dictionary<string, ValidateOneField>))
        {
            Console.WriteLine("");
        }
        
        // model.Sec
    }
}

public class TestDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument model, DocumentFilterContext context)
    {
    }
}

public class TestOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // check it: https://stackoverflow.com/questions/57371528/asp-net-core-2-2-add-locker-icon-only-to-methods-that-require-authorization-sw/61365691#61365691
        // check it: https://stackoverflow.com/questions/60207548/swagger-5-0-idocumentfilter-swaggerdocument-to-openapidocument-custom-defin
        var actionMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;
        var isAuthorized = actionMetadata.Any(metadataItem => metadataItem is AuthorizeAttribute);
        var allowAnonymous = actionMetadata.Any(metadataItem => metadataItem is AllowAnonymousAttribute);


        if (!isAuthorized || allowAnonymous)
        {
            var securityItem = new OpenApiSecurityRequirement();
            operation.Security.Add(securityItem);
        }

        // foreach (var parameterInfo in context.MethodInfo.GetParameters())
        // {
        //     if (parameterInfo.CustomAttributes
        //         .Any(x => x.AttributeType == typeof(FromFormAttribute)))
        //     {
        //         if(operation.RequestBody.Content.ContainsKey(@"application/x-www-form-urlencoded"))
        //         {
        //             operation.RequestBody.Content[@"application/x-www-form-urlencoded"]
        //                     .Encoding[parameterInfo.Name] = new OpenApiEncoding();
        //         }
        //         else
        //         {
        //             operation.RequestBody.Content[@"application/x-www-form-urlencoded"] = new OpenApiMediaType();
        //         }
        //     }
        // }
        // 
    }
}

public class TestParameterFilter : IParameterFilter
{
    public void Apply(OpenApiParameter model, ParameterFilterContext context)
    {
    }
}

public class TestRequestBodyFilter : IRequestBodyFilter
{
    public void Apply(OpenApiRequestBody model, RequestBodyFilterContext context)
    {
    }
}