using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Application.Repositories.Interfaces;
using TaskTracker.Application.Services;
using TaskTracker.Application.Services.Interfaces;
using TaskTracker.Persistence.Repositories;

namespace TaskTracker.Persistence
{
    public static class PersistanceExtension
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskTrackerDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ITaskRepository, TaskRepository>();
            return services;
        }
        public static IApplicationBuilder ConfigurePersistence(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<TaskTrackerDbContext>();

            if (db.Database.IsNpgsql())
            {
                db.Database.Migrate();
            }

            db.Seed().Wait();

            return app;
        }
    }
}