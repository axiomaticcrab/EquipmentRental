using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CQRSlite.Caching;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Events;
using CQRSlite.Messages;
using CQRSlite.Queries;
using CQRSlite.Routing;
using EquipmentRental.Services.PricingService.Domain.CommandHandler;
using EquipmentRental.Services.PricingService.Domain.EventStore;
using EquipmentRental.Services.PricingService.Domain.ReadModel;
using EquipmentRental.Services.PricingService.Domain.ReadModel.Repository.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using ISession = CQRSlite.Domain.ISession;

namespace EquipmentRental.Services.Pricing.PricingService.Command
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var redis = ConnectionMultiplexer.Connect("pricingredis");
            services.AddSingleton(typeof(ConnectionMultiplexer), redis);

            services.AddSingleton(new Router());
            services.AddSingleton<ICommandSender>(x => x.GetService<Router>());
            services.AddSingleton<IEventPublisher>(x => x.GetService<Router>());
            services.AddSingleton<IHandlerRegistrar>(x => x.GetService<Router>());
            services.AddSingleton<IQueryProcessor>(y => y.GetService<Router>());
            services.AddSingleton<IEventStore, InMemoryEventStore>();
            services.AddSingleton<ICache, MemoryCache>();

            
            services.AddScoped<IFeeRepository>(y => new FeeRepository(redis));
            services.AddScoped<ILoyaltyRepository>(y => new LoyaltyRepository(redis));
            services.AddScoped<IPricingRepository>(y => new PricingRepository(redis));
            services.AddScoped<IRepository>(y => new CacheRepository(new Repository(y.GetService<IEventStore>()), y.GetService<IEventStore>(), y.GetService<ICache>()));
            services.AddScoped<IRepository>(y => new CacheRepository(new Repository(y.GetService<IEventStore>()), y.GetService<IEventStore>(), y.GetService<ICache>()));

            services.AddScoped<ISession, Session>();


            services.Scan(scan => scan
                .FromAssemblies(typeof(FeeCommandHandler).GetTypeInfo().Assembly)
                .AddClasses(classes => classes.Where(x =>
                {
                    var allInterfaces = x.GetInterfaces();
                    return
                        allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(IHandler<>)) ||
                        allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(ICancellableHandler<>)) ||
                        allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(IQueryHandler<,>)) ||
                        allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(ICancellableQueryHandler<,>));
                }))
                .AsSelf()
                .WithTransientLifetime()
            );


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpContextAccessor();

            var serviceProvider=services.BuildServiceProvider();

            var registrar = new RouteRegistrar(new Provider(serviceProvider));
            registrar.RegisterInAssemblyOf(typeof(FeeCommandHandler));

            return serviceProvider;
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

    public class Provider : IServiceProvider
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _contextAccessor;

        public Provider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _contextAccessor = _serviceProvider.GetService<IHttpContextAccessor>();
        }

        public object GetService(Type serviceType)
        {
            return _contextAccessor?.HttpContext?.RequestServices.GetService(serviceType) ??
                   _serviceProvider.GetService(serviceType);
        }
    }
}
