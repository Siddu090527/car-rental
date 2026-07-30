using System.Data.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CarRental.Api.Data;

namespace CarRental.Api.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private DbConnection? _connection;

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove the existing AppDbContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            _connection?.Dispose();
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            // Register AppDbContext with SQLite in-memory.
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });
        });

        return base.CreateHost(builder);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            _connection?.Dispose();
        }
    }
}
