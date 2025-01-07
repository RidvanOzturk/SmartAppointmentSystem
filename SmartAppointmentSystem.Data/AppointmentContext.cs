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

    public DbSet<User> Users { get; set; }
    public DbSet<Process> Services { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}


  

