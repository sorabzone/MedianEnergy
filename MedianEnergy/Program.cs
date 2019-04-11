using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MedianEnergy.Engine.Service;
using MedianEnergy.Engine;
using MedianEnergy.Logger;
using MedianEnergy.Engine.Interface;
using MedianEnergy.Engine.OutputService;

namespace MedianEnergy
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //setup our DI
                var serviceProvider = new ServiceCollection()
                    .AddSingleton<LPService>()
                    .AddSingleton<TOUService>()
                    .AddSingleton<IOutputService>(s => new ConsoleOutputService())
                    .AddSingleton<EnergyWorker>()
                    .BuildServiceProvider();

                // reading file path from environment variable
                // environment varibale is easy to pass in CI/CD and DockerCompose
                // we can pass different paths for different deployment, without making any change in any application config files
                var lpFilePath = Environment.GetEnvironmentVariable("LPFiles");
                var touFilePath = Environment.GetEnvironmentVariable("TOUFiles");

                var worker = serviceProvider.GetService<EnergyWorker>();
                await worker.StartProcessing(lpFilePath, touFilePath);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    CommonLogger.LogError(e);
                }
            }
            catch (Exception ex)
            {
                //NLog logger is configured to print error in Console, but we can write in file as well.
                //I write configuration to write log to a file, but commented it for now in NLog.config
                CommonLogger.LogError(ex);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
