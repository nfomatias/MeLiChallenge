using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeLiChallenge.Services;
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
#if DEBUG
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
#endif
#if RELEASE
            var redis = ConnectionMultiplexer.Connect("redis:6379");
#endif
            services.AddScoped<IDatabase>(s => redis.GetDatabase());

            services.AddTransient<IIPInfoService, IPInfoService>();
            services.AddTransient<IIPService, IPService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IExchangeService, ExchangeService>();

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
