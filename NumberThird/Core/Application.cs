using EthicalExploit.Configuration;
using EthicalExploit.Input;
using EthicalExploit.UI;
using EthicalExploit.Logging;
using EthicalExploit.AntiDebug;
using EthicalExploit.Core;
using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace EthicalExploit.Core
{
    public class Application
    {
        private readonly ILogger _logger;
        private readonly ConfigurationManager _configManager;
        private readonly IInputBlocker _inputBlocker;
        private readonly MessageForm _messageForm;
        private readonly ExitConditionHandler _exitConditionHandler;

        public Application(ILogger logger, ConfigurationManager configManager, IInputBlocker inputBlocker, MessageForm messageForm, ExitConditionHandler exitConditionHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configManager = configManager ?? throw new ArgumentNullException(nameof(configManager));
            _inputBlocker = inputBlocker ?? throw new ArgumentNullException(nameof(inputBlocker));
            _messageForm = messageForm ?? throw new ArgumentNullException(nameof(messageForm));
            _exitConditionHandler = exitConditionHandler ?? throw new ArgumentNullException(nameof(exitConditionHandler));
        }

          [STAThread] // Important for Windows Forms!
        public void Run()
        {
            _logger.LogInformation("Application started.");

            // Anti-debug check
            if (DebuggerDetector.IsDebuggerAttached())
            {
                _logger.LogWarning("Debugger detected! Exiting.");
                return;
            }

            // Apply configuration
            var settings = _configManager.AppSettings;
            _messageForm.SetMessage(settings.Message);

            // Start blocking input
            if (settings.InputBlockerType == "Keyboard")
            {
                _inputBlocker.BlockInput();
            }
             else {
                _logger.LogInformation("Input blocking disabled in configuration.");
            }


            // Create and show MessageForm on a separate thread (STA required)
             Thread messageThread = new Thread(() =>
            {
                _messageForm.ShowDialog(); // Use ShowDialog for modal behavior
            });
            messageThread.SetApartmentState(ApartmentState.STA); // Required for Windows Forms
            messageThread.Start();



            // Wait for exit condition
            _exitConditionHandler.WaitForExit();

            // Clean up
            if (settings.InputBlockerType == "Keyboard")
            {
                 _inputBlocker.UnblockInput();
            }

           // Ensure the message thread is terminated gracefully.
           if (messageThread.IsAlive)
           {
               // Use Invoke to safely close the MessageForm from the correct thread.
               _messageForm.Invoke((MethodInvoker)delegate {
                   _messageForm.Close();
               });
               messageThread.Join();  // Wait for the thread to finish.
           }

            _logger.LogInformation("Application stopped.");

        }
    }
}

