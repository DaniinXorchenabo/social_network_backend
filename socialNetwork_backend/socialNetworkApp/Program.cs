using System.Net.Mime;
using System.Text.Json.Serialization;
using dotenv.net;
// using System.Web.Http.Controllers;
using Microsoft.OpenApi.Models;
using socialNetworkApp.api.middlewares;
using socialNetworkApp.docs.swagger;
using Swashbuckle;

// using Microsoft.AspNet.WebApi

class Program
{
    static async Task Main(string[] args)
    {
        
        const string envFileName = ".env";
        
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        while ((baseDirectory = Path.GetDirectoryName(baseDirectory))?.EndsWith("socialNetwork_backend") == false)
        {
            Console.WriteLine(baseDirectory);
        }
        
        Console.WriteLine($"{baseDirectory}/{envFileName}");
            
        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { Path.Combine(baseDirectory, envFileName) }));
        
        var dbHost = System.Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        var dbPort = System.Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
        var dbUsername = System.Environment.GetEnvironmentVariable("DB_SUPERUSER_NAME") ?? "";
        var dbPassword = System.Environment.GetEnvironmentVariable("DB_SUPERUSER_PASSWORD") ?? "";
        var dbName = System.Environment.GetEnvironmentVariable("DB_DATABASE_NAME") ?? "some";
        
        Console.WriteLine($"{dbHost}, {dbPort}, {dbUsername}, {dbPassword}, {dbName}");

        var builder = WebApplication.CreateBuilder(args);
// HttpRequestContext config = new HttpRequestContext();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"});
            c.SchemaFilter<EnumAsStringSchemaFilter>();
            // c.SwaggerGeneratorOptions.SwaggerDocs
            // c.SwaggerGeneratorOptions.SwaggerDocs
        });
        builder.Services.AddSwaggerGenNewtonsoftSupport();

        builder.Services.AddControllers().AddJsonOptions(x =>
        {
            // serialize enums as strings in api responses (e.g. Role)
            x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            // ignore omitted parameters on models to enable optional params (e.g. User update)
            // x.JsonSerializerOptions.IgnoreNullValues = true;
            // x.JsonSerializerOptions.
        });

// builder.Services

        var app = builder.Build();

        app.UseSwagger(c => { });
        app.UseSwaggerUI(c =>
        {
            // c.
            // c.SwaggerEndpoint("docs", "My API V1");
            // c.RoutePrefix = string.Empty;
            // c.DescribeAllEnumsAsStrings();
        });


        app.UseMiddleware<BaseAnswerMiddleware>();
        app.MapControllers();
// app.UseEndpoints(endpoints =>
// {] 
//     endpoints.MapControllers();
// });
        await app.RunAsync();
    }
}