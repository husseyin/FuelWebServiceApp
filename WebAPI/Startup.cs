using Business.Abstract;
using Business.Concrete;
using Business.HangfireJobs;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace WebAPI
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

            /* IOC */

            // OTOBIL SALE
            services.AddScoped<IOtobilService, OtobilManager>();
            services.AddScoped<IOtobilSaleDal, OtobilSaleDal>();

            // FUELCARD FIRMTXN
            services.AddScoped<IFuelCardFirmTxnService, FuelCardFirmTxnManager>();
            services.AddScoped<IFuelCardFirmTxnDal, FuelCardFirmTxnDal>();

            // VEHICLEROG SALETRANS
            services.AddScoped<IVehicleRogSaleTransService, VehicleRogSaleTransManager>();
            services.AddScoped<IVehicleRogSaleTransDal, VehicleRogSaleTransDal>();


            // HANGFIRE
            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

            services.AddHangfireServer();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOtobilService _otobilService, IFuelCardFirmTxnService _fuelCardFirmTxnService, IVehicleRogSaleTransService _vehicleRogSaleTransService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var dashboardOptions = new DashboardOptions
            {
                Authorization = new[]
                {
                    BasicAuthenticationFilter.Authorize(Configuration)
                }
            };

            // HANGFIRE
            app.UseHangfireDashboard("/hangfire", dashboardOptions);

            // OtobilSalesAdd            
            JobsHelper.RecurringJobs("OtobilSalesAdd", () => _otobilService.AddOtobilSales("2023-01-19", "2023-01-20"), Cron.Daily(01, 00));

            // FuelCardFirmTxnAdd
            JobsHelper.RecurringJobs("FuelCardFirmTxnAdd", () => _fuelCardFirmTxnService.AddFuelCardFirmTxns("2023-01-20", "2023-01-20"), Cron.Daily(01, 00));

            // VehicleRogSaleTransAdd
            JobsHelper.RecurringJobs("VehicleRogSaleTransAdd", () => _vehicleRogSaleTransService.AddVehicleRogSaleTrans("2023-01-20", "2023-01-20"), Cron.Daily(01, 00));
        }
    }
}
