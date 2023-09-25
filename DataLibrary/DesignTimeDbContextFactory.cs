using DataLibrary.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataLibrary
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            var connectionString = configuration.GetConnectionString("cloudKitchenConnection");

            builder.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName))
                .UseSnakeCaseNamingConvention();
            return new ApplicationContext(builder.Options);
        }
    }
}
