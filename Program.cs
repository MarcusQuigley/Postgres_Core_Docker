using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Postgres_Core_Docker.DbContexts;
using Postgres_Core_Docker.Extensions;

namespace Postgres_Core_Docker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
            .Build()
            .MigrateAndSeedDatabase<ItemsDbContext>()
            .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
