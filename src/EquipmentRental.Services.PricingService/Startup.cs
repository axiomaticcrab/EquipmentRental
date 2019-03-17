using System;
using EquipmentRental.Services.PricingService.Domain.ReadModel;
using EquipmentRental.Services.PricingService.Domain.ReadModel.Repository.Contract;
using EquipmentRental.Services.PricingService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace EquipmentRental.Services.PricingService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var redis = ConnectionMultiplexer.Connect("pricingredis");
            services.AddSingleton(typeof(ConnectionMultiplexer), redis);

            services.AddScoped<IFeeRepository>(y => new FeeRepository(redis));
            services.AddScoped<ILoyaltyRepository>(y => new LoyaltyRepository(redis));
            services.AddScoped<IPricingRepository>(y => new PricingRepository(redis));

            services.AddHttpClient<IEquipmentService, EquipmentService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
