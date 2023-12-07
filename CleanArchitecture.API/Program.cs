using CleanArchitecture.API.Exceptions;
using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Permet de mettre les routes en minuscule
builder.Services.AddRouting(options => options.LowercaseUrls = true);

#region Logger pour dev
#if DEBUG

ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
    builder.ClearProviders();
    builder.AddConsole();
});
#else
ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddFilter(DbLoggerCategory.Database.Command.Name, level => level == LogLevel.Information);
});
#endif
#endregion

#region Configuration des variables selon l'environnement
// ASPNETCORE_ENVIRONMENT in API/Properties/launchSettings.json
string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
IConfigurationRoot configuration = builder.Configuration
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile($"appsettings.{env}.json", true, true)
                    .Build();
#endregion

#region Connexion BDD
#if DEBUG
string connectionStr = configuration.GetConnectionString("DefaultConnection");
#else
string connectionStr = Environment.GetEnvironmentVariable("MYSQLCONNSTR_MySQLDB");
#endif

builder.Services.AddDatabaseContext(connectionStr);
// builder.Services.AddDatabaseContext(builder.Configuration.GetConnectionString("Database")); 
#endregion

#region Ajout du HttpContext pour pouvoir cancel les requêtes
builder.Services.AddHttpContextAccessor();
#endregion

#region Ajout des services et repositories
builder.Services.AddRepositories();
builder.Services.AddServices();
#endregion

#region Ajout des controlleurs et suppression des objets null dans le flux
builder.Services.AddControllers()
.AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
    opt.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
})
.AddControllersAsServices();
#endregion

#region Ajout de Swagger

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchitecture - Swagger", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
});
#endregion

// Ajout du cache
// builder.Services.AddMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
