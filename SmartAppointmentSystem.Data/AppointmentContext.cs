using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Data.Configurations;
using SmartAppointmentSystem.Data.Entities;
using System.Reflection;
namespace SmartAppointmentSystem.Data;
public class AppointmentContext : DbContext
{
    public AppointmentContext(DbContextOptions<AppointmentContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Process> Processes { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}


  

