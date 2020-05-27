using ImageRecognition.Model.Core.Abstract;
using ImageRecognition.Model.Infrastructure.Services;
using ImageRecognition.Model.Train.Models;
using ImageRecognition.Model.Train.Options;
using ImageRecognition.Model.Train.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.Vision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ImageRecognition.Model.Train
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            var config = LoadConfiguration();

            services.AddOptions<IEnumerable<ModelOptions>>()
               .Configure<IConfiguration>((settings, configuration) =>
               {
                   configuration.GetSection("ModelOptions").Bind(settings);
               });

            var options = config.GetSection("ModelOptions").Get<IEnumerable<ModelOptions>>();
            services.AddLogging(configure => configure.AddConsole());
            services.AddSingleton(config);
            services.AddScoped<IDataService, DataService>();
            foreach (var option in options)
            {
                services.AddScoped<IMLModel, ImageClasificationModel>(x => new ImageClasificationModel(option));
            }
            services.AddScoped<IGetImageService, GetImageFromFolderService>();
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
