using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductMicroservice.DBContexts;
using Microsoft.EntityFrameworkCore;
using ProductMicroservice.Repository;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Prometheus;
using App.Metrics;

namespace ProductMicroservice
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
            //método é passado o nome da chave da cadeia de conexão que foi adicionada ao arquivo appsettings
            services.AddDbContext<ProductContext>(o => o.UseSqlServer(Configuration.GetConnectionString("ProductDB")));

            //método para que a dependência do repositório seja resolvida em tempo de execução, quando necessário.
            services.AddTransient<IProductRepository, ProductRepository>();
            services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });
            //services.AddMetrics();
            //services.AddLogging();
            
            var metrics = AppMetrics.CreateDefaultBuilder().Build();
            services.AddMetrics(metrics);
            services.AddMetricsReportingHostedService();
            
            //Injeçao de dependeência dos tracking de Middleware
            services.AddMetricsTrackingMiddleware();
            services.AddMetricsEndpoints();
            services.AddMetricsReportingHostedService();
            services.AddApplicationInsightsTelemetry();
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseMetricServer("/metrics-text");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Product/Error");
                app.UseHsts();
            }

            app.UseMetricServer();
            app.UseMetricsAllMiddleware();
            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMetricsAllEndpoints();
            app.UseCors(options => { options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            //app.UseCors(options => { options.WithOrigins("127.0.0.1"); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMetrics();
            });
        }
    }
}
