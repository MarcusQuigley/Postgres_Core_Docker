using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Postgres_Core_Docker.DbContexts;

namespace Postgres_Core_Docker
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {

            using (var dbContext = new ItemsDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ItemsDbContext>>()))
            {
                try
                {
                    if (dbContext.Items.Any())
                    {
                        Console.WriteLine("temp database has already been seeded");
                        return;   // DB has been seeded
                    }
                    Console.WriteLine("Seeding temp database");
                    await PopulateTestData(dbContext);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Database hasnt been created yet! Migrate.");
                }
            }
        }

        private static async Task PopulateTestData(ItemsDbContext dbContext)
        {
            await PopulateSomeshite(dbContext);
        }

        private static async Task PopulateSomeshite(ItemsDbContext dbContext)
        {
            string sql = "insert into Items(name ) values " +
                "('1986_1.jpg'), " +
                    " ('2014_1.jpg'); ";

            await dbContext.Database.ExecuteSqlRawAsync(sql);
            //dbContext.Trucks.FromSqlRaw(truckSql);
        }


    }

}