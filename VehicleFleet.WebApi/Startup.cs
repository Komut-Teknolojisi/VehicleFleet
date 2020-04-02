using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VehicleFleet.Application;
using VehicleFleet.Application.Vehicles;
using VehicleFleet.Domain.Entities;
using VehicleFleet.Domain.interfaces;
using VehicleFleet.Infrastructure.Data;
using VehicleFleet.Infrastructure.Data.EfCore;
using VehicleFleet.WebApi.Logging;

namespace VehicleFleet.WebApi
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
            services.AddDbContext<VehicleFleetContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));

            services.AddMemoryCache();
            services.AddControllers();

            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<IVehicleRepository, EfVehicleRepository>();
            services.AddScoped<IModelRepository, EfModelRepository>();
            services.AddScoped<IBrandRepository, EfBrandRepository>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IRepository<BaseEntity>, EfBaseReporitory<BaseEntity>>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddProvider(new LoggerProvider(env));

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<VehicleFleetContext>().Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
