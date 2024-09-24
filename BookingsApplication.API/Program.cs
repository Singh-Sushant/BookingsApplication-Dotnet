using BookingsApplication.API.Data;
using BookingsApplication.API.Mappings;
using BookingsApplication.API.Repositories;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookingAppDBcontext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BookingsApplicationConnectionString")));

builder.Services.AddScoped<IEventRepository,SQLEventRepository>();
builder.Services.AddScoped<IBookingsRepository,SQLBookingsRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200") // Replace with your Angular app URL
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
