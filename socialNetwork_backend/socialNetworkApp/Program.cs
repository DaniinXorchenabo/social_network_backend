using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using socialNetworkApp.api.middlewares;
using socialNetworkApp.config;
using socialNetworkApp.docs.swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using socialNetworkApp.db;
using Swashbuckle.AspNetCore.SwaggerGen;

// using System.Web.Http.Controllers;

// using Microsoft.AspNet.WebApi

namespace socialNetworkApp;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // добавление сервисов аутентификации
        builder.Services.AddSingleton<Env>();
        var env1 = new Env();

        // HttpRequestContext config = new HttpRequestContext();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
            {
                // check it: https://stackoverflow.com/questions/43447688/setting-up-swagger-asp-net-core-using-the-authorization-headers-bearer
                // check it: https://medium.com/nerd-for-tech/authentication-and-authorization-in-net-core-web-api-using-jwt-token-and-swagger-ui-cc8d05aef03c
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "IBook backend API",
                    Description = "It is API for social network",
                    // TermsOfService = new Uri("https://example.com/terms"),
                    // Contact = new OpenApiContact
                    // {
                    //     Name = "Shayne Boyer",
                    //     Email = string.Empty,
                    //     Url = new Uri("https://twitter.com/spboyer"),
                    // },
                    // License = new OpenApiLicense
                    // {
                    //     Name = "Use under LICX",
                    //     Url = new Uri("https://example.com/license"),
                    // }
                });
                // IDocumentFilter


                // c.SwaggerGeneratorOptions.SwaggerDocs
                // c.SwaggerGeneratorOptions.SwaggerDocs

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });


                c.SchemaFilter<EnumAsStringSchemaFilter>();
                c.DocumentFilter<TestDocumentFilter>();
                c.OperationFilter<TestOperationFilter>();
                c.ParameterFilter<TestParameterFilter>();
                c.RequestBodyFilter<TestRequestBodyFilter>();
                // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // c.IncludeXmlComments(xmlPath);
            }
        );

        // builder.Services.AddAuthentication("Bearer") // схема аутентификации - с помощью jwt-токенов
        // .AddJwtBearer(); // подключение аутентификации с помощью jwt-токенов

        builder.Services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = env1.Backend.Address,
                ValidAudience = env1.Backend.Address,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(env1.Backend.SecretKey)) //Configuration["JwtToken:SecretKey"]  
            };
        });
        builder.Services.AddAuthorization(); // добавление сервисов авторизации


        builder.Services.AddSwaggerGenNewtonsoftSupport();

        builder.Services.AddControllers().AddJsonOptions(x =>
        {
            // serialize enums as strings in api responses (e.g. Role)
            x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            // ignore omitted parameters on models to enable optional params (e.g. User update)
            // x.JsonSerializerOptions.IgnoreNullValues = true;
            // x.JsonSerializerOptions.
        });

        builder.Services.AddDbContext<BaseBdConnection>();
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();


        // builder.Services

        var app = builder.Build();


        // env.
        using (IServiceScope scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            scope.ServiceProvider.GetService<BaseBdConnection>()?.Database.Migrate();
        }


        app.UseMiddleware<BaseAnswerMiddleware>();
        app.MapControllers();

        app.UseAuthentication(); // добавление middleware аутентификации
        app.UseAuthorization(); // добавление middleware авторизации 
        app.UseSwagger(c => { });
        app.UseSwaggerUI(c =>
        {
            // c.
            // c.SwaggerEndpoint("docs", "My API V1");
            // c.RoutePrefix = string.Empty;
            // c.DescribeAllEnumsAsStrings();
        });

        var envs = app.Services.GetService<Env>();
        Console.WriteLine($"{envs.Db.DbUsername}, {envs.Db.DbPassword}");
        Console.WriteLine($"{envs.Backend.BackendHostRunnable}, {envs.Backend.BackendPortInternal}");
        // app.UseEndpoints(endpoints =>
        // {] 
        //     endpoints.MapControllers();
        // });
        await app.RunAsync();
    }
}