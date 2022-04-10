using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using socialNetworkApp.api.middlewares;
using socialNetworkApp.config;
using socialNetworkApp.docs.swagger;
using Microsoft.EntityFrameworkCore;
using socialNetworkApp.db;

// using System.Web.Http.Controllers;

// using Microsoft.AspNet.WebApi

namespace socialNetworkApp;

class Program
{
    static async Task Main(string[] args)
    {
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
        builder.Services.AddSingleton<Env>();
        builder.Services.AddDbContext<BaseBdConnection>();
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        

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
        var envs = app.Services.GetService<Env>(); 
        Console.WriteLine($"{envs.Db.DbUsername}, {envs.Db.DbPassword}");
        // app.UseEndpoints(endpoints =>
        // {] 
        //     endpoints.MapControllers();
        // });
        await app.RunAsync();
    }
}