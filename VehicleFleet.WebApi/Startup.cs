using AutoMapper;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using System;
using System.Linq;
using VehicleFleet.Application;
using VehicleFleet.Application.Vehicles;
using VehicleFleet.Domain.Entities;
using VehicleFleet.Domain.interfaces;
using VehicleFleet.Infrastructure.Data;
using VehicleFleet.Infrastructure.Data.EfCore;
using VehicleFleet.Infrastructure.Logging;

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
            services.AddDbContext<VehicleFleetContext>(options => options
                .UseSqlServer(Configuration["ConnectionString"])
                );

            services.AddMemoryCache();
            services.AddCors(options =>
            {
                options.AddPolicy("allowCors", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            //services.AddControllers(options => options.EnableEndpointRouting = false).AddNewtonsoftJson();
            services.AddOData();
            services.AddMvc(options => options.EnableEndpointRouting = false).AddNewtonsoftJson();

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
            loggerFactory.AddProvider(new LoggerProvider());

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

            app.UseCors("allowCors");

            app.UseAuthorization();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel(app.ApplicationServices));
                routeBuilder.EnableDependencyInjection();
            });
        }

        private static IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);
            builder.EntitySet<VehicleDto>("Vehicles");
            builder.EntitySet<BrandDto>("Brands");
            builder.EntitySet<ModelDto>("Models");

            return builder.GetEdmModel();
        }
    }
}