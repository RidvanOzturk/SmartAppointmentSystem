using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SmartAppointmentSystem.Data
{
    public class AppointmentContextFactory : IDesignTimeDbContextFactory<AppointmentContext>
    {
        public AppointmentContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            var configFileName = $"appsettings.{env}.json";
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(configFileName, optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("AppointmentContext");
            var optionsBuilder = new DbContextOptionsBuilder<AppointmentContext>();

            if (env.Equals("Development", StringComparison.OrdinalIgnoreCase))
            {
                optionsBuilder.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly("SmartAppointmentSystem.Data"));
            }
            else
            {
                optionsBuilder.UseNpgsql(connectionString,
                    b => b.MigrationsAssembly("SmartAppointmentSystem.Data"));
            }

            return new AppointmentContext(optionsBuilder.Options);
        }
    }
}
