using App.Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductMicroservice.DBContexts;
using ProductMicroservice.Repository;
using static ProductService.gRPC;

namespace ProductMicroservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            System.Net.ServicePointManager.Expect100Continue = false;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ProductContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("ProductServer")));

            //services.AddTransient<gRPCBase, ProductRepository>();

            var metrics = new MetricsBuilder()
            .OutputMetrics.AsPrometheusProtobuf()
            .OutputMetrics.AsPrometheusPlainText()
            .Build();

            services.AddApplicationInsightsTelemetry();
            services.AddMvc();
            services.AddMetrics(metrics);
            services.AddMetricsEndpoints();
            services.AddMetricsTrackingMiddleware();
            services.AddMetricsReportingHostedService();            
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {        
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        
            app.UseMetricsAllEndpoints();
            app.UseMetricsAllMiddleware();      
            app.UseRouting();
            app.UseGrpcWeb();
            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ProductRepository>().EnableGrpcWeb();    
            });
        }
    }
}
