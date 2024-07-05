using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebApi.Data;
using Testcontainers.MsSql;

namespace SimpleWebApi.IntegrationTests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithName("SimpleWebApiDb")
        .WithPassword("Strong_password_123!")
        .Build();


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Add services
        });

        builder.ConfigureTestServices(services =>
        {
            // Replace services
            var descriptorType =
                typeof(DbContextOptions<SimpleWebApiDbContext>);

            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == descriptorType);

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }


            services.AddDbContext<SimpleWebApiDbContext>(options =>
            {
                options.UseSqlServer(_dbContainer.GetConnectionString(), sqlServerOptions =>
                {
                });
            });


            //using var scope = services.BuildServiceProvider().CreateScope();
            //var serviceProvider = scope.ServiceProvider;
            //var context = serviceProvider.GetRequiredService<SimpleWebApiDbContext>();
            //context.Database.EnsureCreated();
        });

        builder.UseEnvironment("Development");
    }

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}
