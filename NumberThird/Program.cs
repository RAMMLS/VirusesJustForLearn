using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EthicalExploit.Core;
using EthicalExploit.Logging;
using EthicalExploit.Configuration;
using EthicalExploit.Input;
using EthicalExploit.UI;
using System;

namespace EthicalExploit
{
    public class Program
    {
        [STAThread] // Required for Windows Forms
        static void Main(string[] args)
        {
            // Configure dependency injection
            var services = new ServiceCollection()
                .AddSingleton<ILogger, Logger>()
                .AddSingleton<ConfigurationManager>()
                .AddSingleton<IInputBlocker, KeyboardBlocker>()
                .AddSingleton<ConsoleInterface>()
                .AddSingleton<MessageForm>()
                .AddSingleton<ExitConditionHandler>()
                .AddSingleton<Application>();

            var serviceProvider = services.BuildServiceProvider();

            // Resolve the Application class and run the application
            var app = serviceProvider.GetRequiredService<Application>();
            app.Run();


        }
    }
}

