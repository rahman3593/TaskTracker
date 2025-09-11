using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Application.Services;
using TaskTracker.Application.Services.Interfaces;

namespace TaskTracker.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            
            services.AddTransient<ITaskService, TaskService>();
            return services;
        }
    }
}
