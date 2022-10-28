using KodlamaDevs.Application;
using KodlamaDevs.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using OniCore.CrossCuttingConcerns.Exceptions;
using OniCore.CrossCuttingConcerns.Exceptions.Handlers;
using OniCore.Security;
using OniCore.Security.Encryption;
using OniCore.Security.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog configuration, for more settings see appsetting.json

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog(logger);

// Add services to the container

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSecurityServices();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<HttpExceptionHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.AddSecurityDefinition("JWT Authorization",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Description = "Enter the `Generated-JWT-Token`:",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        });

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Id = "JWT Authorization", Type = ReferenceType.SecurityScheme }
            },
            Array.Empty<string>()
        }
    });
});

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpLogging(); // for more detailed logging

//if (app.Environment.IsProduction())
    app.ConfigureCustomExceptionMiddleware();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();