using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using System.Text.Json.Serialization;
using PreventorTest.Application.Students.Interfaces;
using PreventorTest.Infrastructure.Repositories;
using PreventorTest.Infrastructure.EntityFramework.Contexts;

namespace Project.Api
{
    public class Startup
    {
        string connectionString = "Server=localhost;Database=Students;TrustedConnection=true";

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            services.AddEndpointsApiExplorer();

            services.AddDbContextFactory<PreventorDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("PreventorConnection"));
            }, ServiceLifetime.Transient);

            services.AddScoped<IStudentRepository, StudentRepository>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options =>
            {
                options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}