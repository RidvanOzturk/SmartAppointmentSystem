using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Api.Models.Validators;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.Implementations;
using SmartAppointmentSystem.Data;
using FluentValidation;
using SmartAppointmentSystem.Api.Extensions;
using Microsoft.AspNetCore.RateLimiting;
using SmartAppointmentSystem.Api.Middlewares;
using SmartAppointmentSystem.Api.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", limiterOptions =>
    {
        limiterOptions.PermitLimit = 20;
        limiterOptions.Window = TimeSpan.FromSeconds(10);
    });
});
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
builder.Services.AddScoped<ILoggingService, LoggingService>();

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
builder.Services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppointmentContext>();
    dbContext.Database.Migrate();
}
app.UseRateLimiter();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
