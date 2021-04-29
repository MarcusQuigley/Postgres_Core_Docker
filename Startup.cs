using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Postgres_Core_Docker.DbContexts;

namespace Postgres_Core_Docker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddADbContext(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Postgres_Core_Docker", Version = "v1" });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Postgres_Core_Docker v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class ServiceDbCont
    {
        public static IServiceCollection AddADbContext(this IServiceCollection services, IConfiguration configuration)
        {
            //psql -h localhost -U marcus -p 5432 -d items_db
            var server = configuration["POSTGRES_SERVER"];// ?? "localhost";

            var port = configuration["POSTGRES_PORT"];// ?? "5432";
            var database = configuration["POSTGRES_DB"];// ?? "hess_catalog_db";


            var user = configuration["POSTGRES_USER"];// ?? "marcus";
            var password = configuration["POSTGRES_PASSWORD"];// ?? "password";
            var connectionString = $"Server={server};Port={port};User ID={user};Password={password};Database={database};Pooling=true";
            Console.WriteLine($"CONNECTION STRING Catalog: {connectionString}");

            //"User ID =postgres;Password=password;Server=localhost;Port=5432;Database=testDb;Integrated Security=true;Pooling=true;" //alternative
            services.AddDbContext<ItemsDbContext>(options =>
                options.UseNpgsql(connectionString)
                     .UseSnakeCaseNamingConvention()
                    );
            return services;
        }
    }
}
