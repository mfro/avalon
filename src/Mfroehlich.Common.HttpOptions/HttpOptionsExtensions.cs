using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Mfroehlich.Common.HttpOptions
{
    public static class HttpOptionsExtensions
    {
        private const string ConfigFile = "config.json";

        public static IWebHostBuilder UseHttpOptions(this IWebHostBuilder build)
        {
            return build.UseHttpOptions<HttpOptions>();
        }

        public static IWebHostBuilder UseHttpOptions<T>(this IWebHostBuilder build) where T : HttpOptions
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(ConfigFile)
                .Build();

            var options = config.Get<T>();

            build.UseUrls($"http://+:{options.Port}");

            build.ConfigureServices(services => {
                services.AddOptions();
                services.Configure<T>(config);
            });

            // build.ConfigureLogging(logging => {
            //     logging.AddConsole(LogLevel.Warning);

            //     logging.AddFile(Path.Combine(options.Logs, "errors.log"), LogLevel.Warning);
            //     logging.AddFile(Path.Combine(options.Logs, "info.log"), LogLevel.Information);
            // });

            return build;
        }
    }
}
