using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Torvnen.UrlScanner.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var hostRunTask = host.RunAsync();
            
            using var scope = host.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();
            logger.LogInformation(@"
 / ____|                                     /\   |  __ \_   _|
| (___   ___ __ _ _ __  _ __   ___ _ __     /  \  | |__) || |  
 \___ \ / __/ _` | '_ \| '_ \ / _ \ '__|   / /\ \ |  ___/ | |  
 ____) | (_| (_| | | | | | | |  __/ |     / ____ \| |    _| |_ 
|_____/ \___\__,_|_| |_|_| |_|\___|_|    /_/    \_\_|   |_____|
                                        Is now running!");

            logger.LogInformation("Tip! Navigate to the root of the API to access the Swagger documentation.");

            await hostRunTask;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
