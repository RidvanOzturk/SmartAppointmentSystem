using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SmartAppointmentSystem.Data
{
    public class AppointmentContextFactory : IDesignTimeDbContextFactory<AppointmentContext>
    {
        public AppointmentContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.Production.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("AppointmentContext");

            var optionsBuilder = new DbContextOptionsBuilder<AppointmentContext>();

            optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("SmartAppointmentSystem.Data"));

            return new AppointmentContext(optionsBuilder.Options);
        }
    }
}
