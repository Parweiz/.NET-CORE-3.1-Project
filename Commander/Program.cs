using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Commander
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    // https://www.mssqltips.com/sqlservertip/6348/securely-manage-database-credentials-using-visual-studio-manage-user-secrets/
                    .ConfigureAppConfiguration((context, config) =>
                    {
                        var env = context.HostingEnvironment;
                        config.AddJsonFile("appsettings.json").AddJsonFile($"appsettings.{env.EnvironmentName}.json");
                        config.AddEnvironmentVariables();
                       
                    })
                    .UseStartup<Startup>();
                });
    }
}
