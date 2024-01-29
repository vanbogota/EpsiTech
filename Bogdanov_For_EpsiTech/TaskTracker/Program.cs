using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskTracker.Context;
using TaskTracker.Models;
using TaskTracker.Services;
using TaskTracker.Services.Impl;

namespace TaskTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            #region Configure DB Context

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

            #endregion

            #region Register services in DI

            builder.Services.AddScoped<ITaskTrackerService<MyTask>, TaskTrackerService>();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Task Tracker",
                    Version = "v1"
                });
            });

            builder.Services.AddControllers();

            #endregion

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.MapControllers();
            app.Run();
        }
    }
}
