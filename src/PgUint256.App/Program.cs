using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PgUint256.App.HostedServices;
using PgUint256.Data;

await Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<Db>(options =>
        {
            // docker run --name postgres -p 5432:5432 -e POSTGRES_PASSWORD=password -d postgres

            options.UseNpgsql("Server=localhost;Port=5432;Database=PgUint256;User ID=postgres;Password=password");
        });

        services.AddHostedService<UseCase>();
    })
    .RunConsoleAsync();