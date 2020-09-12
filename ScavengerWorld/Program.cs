using Serilog;
using System;

namespace ScavengerWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Debug()
                                .WriteTo.Console()
                                .WriteTo.File("logs\\application.log", rollingInterval: RollingInterval.Minute)
                                .CreateLogger();

            try
            {
                RunSimulation();
                Log.Information("Simulation Complete, press any key to exit");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unhandled Exception:");
            }

            Log.CloseAndFlush();

            Console.ReadLine();
        }

        private static void RunSimulation()
        {
            var simulator = new Simulator();
            simulator.Run();
        }
    }
}
