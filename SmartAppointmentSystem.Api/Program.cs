using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Api.Models.Validators;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.Implementations;
using SmartAppointmentSystem.Data;
using FluentValidation;
using SmartAppointmentSystem.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContext<AppointmentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppointmentContext")));
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPatientUserService, PatientUserService>();
builder.Services.AddScoped<IDoctorUserService, DoctorUserService>();
builder.Services.AddScoped<IProcessService, ProcessService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddValidatorsFromAssemblyContaining<PatientUserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AppointmentValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TimeSlotValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProcessValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RatingValidator>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.RegisterJWTAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
