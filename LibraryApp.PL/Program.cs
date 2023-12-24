using LibraryApp.BLL;
using LibraryApp.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LibraryApp.PL
{
    public class Program
    {
        static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            var applicationTask = services.GetRequiredService<Application>().Start();

            applicationTask.GetAwaiter().GetResult();
        }

        private static IHostBuilder CreateHostBuilder(string[] strings)
        {
            #region IF_CONSOLE_APP
            // var configDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //                                       .SetBasePath(configDirectory)
            //                                       .AddJsonFile("appsettings.json")
            //                                        .Build();
            //var connectionString = configuration.GetConnectionString("SqlServerConnectionString");

            #endregion

            #region IF_APPLYING_MIGRATION
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=mylibrary;Trusted_Connection=True;MultipleActiveResultSets=true";
            #endregion

            return Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddScoped<Application>();
                    services.AddBusinessLogicLayer();
                    services.AddDataAccessLayer(connectionString);
                });
        }
    }
}