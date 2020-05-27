using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;

namespace ImageRecognition.App
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            var config = LoadConfiguration();


            services.AddLogging(configure => configure.AddConsole());
            services.AddSingleton(config);
            services.AddTransient<App>();

            var provider = services.BuildServiceProvider();
            var app = provider.GetRequiredService<App>();
            app.Run();
        }

        private static IConfiguration LoadConfiguration()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets(assembly);

            return builder.Build();
        }
    }
}
