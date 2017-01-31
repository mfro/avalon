using Mfroehlich.Avalon.Game;
using Mfroehlich.Common.HttpOptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Mfroehlich.Avalon
{
    public class Startup
    {
        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseHttpOptions<AvalonOptions>()
                .UseStartup<Startup>()
                .UseKestrel()
                .Build();

            host.Run();
        }

        private AvalonOptions options;

        public Startup(IOptions<AvalonOptions> options)
        {
            this.options = options.Value;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSingleton<GameManager>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseWebSockets();
            app.UseCors(builder => {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.WithOrigins(options.Origins);
            });

            app.UseMiddleware<SocketHandler>();
        }
    }
}