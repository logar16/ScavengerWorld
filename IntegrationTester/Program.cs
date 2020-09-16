using Serilog;
using System;

namespace IntegrationTests
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup logging
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

        private static void RunAllTests()
        {
            throw new NotImplementedException();
        }

        private static void RunTest(string testName)
        {
            throw new NotImplementedException();
        }

        private static void RunSimulation()
        {
            var simulator = new Simulator("Config/Worlds/test-world.json");
            simulator.Run();
        }
    }
}
