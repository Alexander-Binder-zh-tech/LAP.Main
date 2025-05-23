using System.Text.Json.Serialization;
using LAP.Main.Components;
using LAP.Main.Contexts;
using LAP.Security.Authentication;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

//Configure NLog
builder.Logging.ClearProviders();
NLog.LogManager.Configuration = new NLogLoggingConfiguration(builder.Configuration.GetSection("NLog"));
builder.Host.UseNLog();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.SetIsOriginAllowed(origin => new Uri(origin).IsLoopback)
                .AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyMethod();
        });
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


//Configure MVC services -> https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.mvcservicecollectionextensions.addcontrollers?view=aspnetcore-9.0&viewFallbackFrom=net-8.0
builder.Services.AddControllers().AddJsonOptions(opts =>
{
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter the token with the `Bearer: ` prefix, e.g. \"Bearer abcde12345\"."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("Db")!;

//Configure authentication
builder.Services.AddCustomAuthentication(new AuthenticationConfig
{
    ConnectionString = connectionString,
    JwtKey = builder.Configuration.GetValue<string>("Jwt:Key")!,
    JwtIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer")!,
    ConfigBuilder = builder.Configuration
});

//Configure db models + contexts with EF migrations
ModelContextHelper.ConfigureModelContexts(builder, connectionString);

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetValue<string>("url")!.Split(';').FirstOrDefault()!)
});

var app = builder.Build();

//Trigger DB migration
ModelContextHelper.MigrateModelDb(app);

//TODO add authentication system for user management
//TODO add and register audit service

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var logger = app.Services.GetService<ILogger<Program>>()!;
app.UseStatusCodePages(context =>
{
    var code = context.HttpContext.Response.StatusCode;
    if (code == 404)
    {
        logger.LogError("Resource not found - {Path}", context.HttpContext.Request.Path);
    }

    return Task.CompletedTask;
});


app.UseCors();
//app.UseHttpsRedirection(); TODO


app.UseStaticFiles();
app.MapControllers();

app.UseCustomAuthentication();

app.MapRazorComponents<App>().DisableAntiforgery()
    .AddInteractiveServerRenderMode();

app.Run();