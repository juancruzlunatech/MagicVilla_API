//using Serilog;
using MagicVilla_VillaAPI;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Repository;
using MagicVilla_VillaAPI.Repository.IvillaRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<ApplicationDbContext>(option => { option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")); });
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = true;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("ApiSettings:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true; // Add info about version to the Http responses
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Serilog Logging
// Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File
//     ("Log/Villalog.txt", rollingInterval: RollingInterval.Day).CreateLogger();
// builder.Host.UseSerilog();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme.\r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
            "Example: \"Bearer 23423l4j2l34k2j34lj\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new List<string>()
        }
    });
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Magic Villa API",
        Version = "v1.0",
        Description = "Magic Villa API by Juan",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Juan",
            Url = new Uri("https://example.com/contact"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "Magic Villa API V2",
        Version = "v2.0",
        Description = "Magic Villa API by Juan V2",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Juan",
            Url = new Uri("https://example.com/contact"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
});

builder.Services.AddSingleton<ILogging, Logging>();

var app = builder.Build(); ;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Magic Villa API V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Magic Villa API V2");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();