using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using MeLiChallenge.Services;
using MeLiChallenge.Services.Externals;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace MeLiChallenge
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

            services.AddMvc()
                .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

            string connectionString= Configuration.GetValue<string>(key: SettingKeys.RedisConnection ); //"redis:6379";

            services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(connectionString));

            services.AddSingleton<ICacheService, RedisCacheService>();

            services.AddTransient<IIPGurardService, IPGuardService>();
            services.AddTransient<IIPService, IPService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IExchangeService, ExchangeService>();
            services.AddSingleton<IReferenceCountryService, ReferenceCountryService>();
            services.AddTransient<IStatisticService, StatisticService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
