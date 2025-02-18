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
            // Tüm entity configuration'ları uygula (AppointmentConfiguration dahil)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Veritabanı sağlayıcısını al
            var provider = this.Database.ProviderName;
            Console.WriteLine("Database Provider: " + provider); // Debug amaçlı

            // Eğer PostgreSQL değilse (SQL Server gibi) "uuid" ayarını kaldır
            if (!string.IsNullOrEmpty(provider) && !provider.Contains("Npgsql"))
            {
                // Appointment entity'sindeki ilgili sütunların tipini sıfırlıyoruz
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
