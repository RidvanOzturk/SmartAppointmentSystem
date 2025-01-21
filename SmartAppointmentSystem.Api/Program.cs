using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Api.Models.Validators;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.Implementations;
using SmartAppointmentSystem.Data;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Ortam deðiþkenleri daha sonra yüklenir
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContext<AppointmentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppointmentContext")));
Console.WriteLine($"Environment Variable (Direct): {Environment.GetEnvironmentVariable("AppSettings__Secret")}");
Console.WriteLine($"Configuration Value: {builder.Configuration["AppSettings:Secret"]}");

builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProcessService, ProcessService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
