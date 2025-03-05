using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Api.Models.Validators;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.Implementations;
using SmartAppointmentSystem.Data;
using FluentValidation;
using SmartAppointmentSystem.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("AppointmentContext");


builder.Services.AddDbContext<AppointmentContext>(options =>
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("SmartAppointmentSystem.Data")));


builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IPatientUserService, PatientUserService>();
builder.Services.AddScoped<IDoctorUserService, DoctorUserService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddValidatorsFromAssemblyContaining<PatientUserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AppointmentValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TimeSlotValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RatingValidator>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddOpenApi();
builder.Services.RegisterJWTAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
