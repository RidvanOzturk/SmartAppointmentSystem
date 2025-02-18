using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Data.Configurations;
using SmartAppointmentSystem.Data.Entities;
using System.Reflection;

namespace SmartAppointmentSystem.Data
{
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

            var provider = this.Database.ProviderName;
            Console.WriteLine("Database Provider: " + provider); 

            if (!string.IsNullOrEmpty(provider) && !provider.Contains("Npgsql"))
            {
                modelBuilder.Entity<Appointment>().Property(a => a.Id).HasColumnType(null);
                modelBuilder.Entity<Appointment>().Property(a => a.DoctorId).HasColumnType(null);
                modelBuilder.Entity<Appointment>().Property(a => a.PatientId).HasColumnType(null);
                modelBuilder.Entity<Doctor>().Property(a => a.Id).HasColumnType(null);
                modelBuilder.Entity<Patient>().Property(a => a.Id).HasColumnType(null);
                modelBuilder.Entity<Process>().Property(a => a.Id).HasColumnType(null);
                modelBuilder.Entity<Process>().Property(a => a.DoctorId).HasColumnType(null);
                modelBuilder.Entity<Rating>().Property(a => a.Id).HasColumnType(null);
                modelBuilder.Entity<Rating>().Property(a => a.DoctorId).HasColumnType(null);
                modelBuilder.Entity<Rating>().Property(a => a.PatientId).HasColumnType(null);
                modelBuilder.Entity<TimeSlot>().Property(a => a.Id).HasColumnType(null);
                modelBuilder.Entity<TimeSlot>().Property(a => a.DoctorId).HasColumnType(null);

            }

            base.OnModelCreating(modelBuilder);
        }


    }
}
