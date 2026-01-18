using ProjectDigitization.Engine;
using ProjectDigitization.Engine.Interfaces;
using ProjectDigitization.Engine.Models;
using ProjectDigitization.Engine.Services;
using ProjectDigitization.Interfaces.Logging;
using ProjectDigitization.Services.Logging;
using ProjectDigitization.ViewModels.ViewModels;

namespace ProjectDigitization.Engine
{
    public class Program
    {
        private static string _logFilepath = string.Empty;
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // Ensure the base path is the content root of the process
                    var env = hostingContext.HostingEnvironment;
                    config.SetBasePath(Directory.GetCurrentDirectory());

                    // Core appsettings (already loaded by CreateDefaultBuilder, but safe to re-add)
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                    // Environment specific
                    config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    // Engine-specific override file (optional)
                    config.AddJsonFile("appsettings.Engine.json", optional: true, reloadOnChange: true);

                    // Also load the engine project's appsettings.json if it's placed in the project folder
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                    // Environment variables (ASPNETCORE_*, QUEUE settings via __)
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    try
                    {
                        IConfiguration configuration = hostContext.Configuration;
                        services.Configure<AppsettingsForEngine>(hostContext.Configuration);
                        var _appsettings = configuration.Get<AppsettingsForEngine>();
                        _logFilepath = "E:\\ProjectDigitizationEngineLogs";


                        // register your logger / services (ensure types exist)
                        // register concrete instance for direct injection (alternative to IOptions)
                        services.AddSingleton(configuration.Get<AppsettingsForEngine>());
                        services.AddSingleton(typeof(IGenericLogger<>), typeof(GenericLogger<>));

                        services.Configure<QueueSettingsForEngine>(hostContext.Configuration.GetSection("QueueSettings"));

                        services.AddSingleton<IHttpService, HttpService>();

                        services.AddHostedService<Worker>();
                        services.AddSingleton<IGenerator, Generator>();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                });
    }
}
