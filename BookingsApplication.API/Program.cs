using System.Text;
using api.Service;
using BookingsApplication.API.CustomActionFilter;
using BookingsApplication.API.Data;
using BookingsApplication.API.Mappings;
using BookingsApplication.API.Models.Domains;
using BookingsApplication.API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// CORS policy configuration name
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database context configuration
builder.Services.AddDbContext<BookingAppDBcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingsApplicationConnectionString"))
    .ConfigureWarnings(warnings =>
        warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
);

// Identity and authentication setup
builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<BookingAppDBcontext>();

// Repositories and AutoMapper configuration
builder.Services.AddScoped<IEventRepository, SQLEventRepository>();
builder.Services.AddScoped<IBookingsRepository, SQLBookingsRepository>();
builder.Services.AddScoped<ICartRepository, SQLCartRepository>();
builder.Services.AddScoped<ICheckoutService, CheckoutService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// JWT-based authentication configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Enable authorization services
builder.Services.AddAuthorization();

// Global action filter configuration
builder.Services.AddControllers(options =>
{ 
    options.Filters.Add<ValidateModel>();
});

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost",
                                      "http://localhost:4200",
                                      "https://localhost:7230",
                                      "http://localhost:90")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});

// Swagger configuration
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Bookings API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });



var app = builder.Build();

// Map Identity API endpoints if needed
app.MapIdentityApi<User>();

// HTTP request pipeline configuration
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

// Development-specific configurations
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map API controllers
app.MapControllers();

app.Run();
