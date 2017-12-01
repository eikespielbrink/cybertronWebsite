using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace ServeOrDie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                BuildWebHost(args).Run();
            }
            catch{ }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
#if DEBUG
            .UseUrls("http://localhost:53937/")
#endif
                .UseKestrel()
                .CaptureStartupErrors(true)
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}
