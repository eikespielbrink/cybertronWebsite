using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ServeOrDie
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme);

            services.AddApplicationInsightsTelemetry(Configuration);

            // AppSettings lesen
            CybertronConfig.PathOfEvolution4 = Configuration["ConvertPortalConfig:PathOfEvolution4"] ?? "C:\\CoreTechnologie\\evolution-4.0\\evolution64\\evolution4.exe";
            CybertronConfig.PathOfConvertScript = Configuration["ConvertPortalConfig:PathOfConvertScript"] ?? "C:\\Program Files\\IcarusConvertPortal\\ConvertPortal.scp";
            CybertronConfig.WorkingPathofEvolution4 = Configuration["ConvertPortalConfig:WorkingPathofEvolution4"] ?? "C:\\CoreTechnologie\\evolution-4.0\\lib64";
            CybertronConfig.WebAppUrl = Configuration["ConvertPortalConfig:WebAppUrl"] ?? "http://*:53937";
            CybertronConfig.DebugURL = Configuration["ConvertPortalConfig:DebugURL"] ?? "http://localhost:53937";
            CybertronConfig.ReleaseURL = Configuration["ConvertPortalConfig:ReleaseURL"] ?? "http://ibl165:53937";

            services.AddMvc();
        }

        public IConfigurationRoot Configuration { get; }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc();
        }
    }
}
