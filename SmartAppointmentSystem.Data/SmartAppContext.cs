using SmartAppointmentSystem.Data.Configurations;
using SmartAppointmentSystem.Data.Entities;
using System.Data.Entity;

public class AppointmentDbContext : DbContext
{
    public AppointmentDbContext() : base("name=SmartAppContext")
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Configurations.Add(new UserConfiguration());
        modelBuilder.Configurations.Add(new ServiceConfiguration());
        modelBuilder.Configurations.Add(new AppointmentConfiguration());
        modelBuilder.Configurations.Add(new RatingConfiguration());
        modelBuilder.Configurations.Add(new TimeSlotConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}