using System;
using EthicalExploit.Configuration;
using EthicalExploit.UI;
using EthicalExploit.Logging;
using System.Threading;
using System.Windows.Forms;

namespace EthicalExploit.Core {
  public clss ExitConditioHandler {
    private readonly AppSettings _settings;
    private readonly ConsoleInterface _consoleInterface;
    private readonly ILogger _logger;

    public ExitConditioHandler(AppSettings settings, ConsoleInterface consoleInterface, ILogger logger) {
      _settings = settings ?? throw new ArgumentNullException(nameof(settings));
      _consoleInterface = consoleInterface ??throw new ArgumentNullException(nameof(consoleInterface));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void WaitForExit() {
      switch (_settins.ExitConditionType) {
        case "Password":
          WaitForPassword(); break;
        case "KeyCombination": WaitForSpecialKeyCombination(); break;
        default: _logger.LogWarning($"Unknown ExitConditionType: {_settings.ExitConditionType}. Exiting immediately."); break;
      }
    }

    private void WaitForPssword() {
      Conole.WriteLine("Введите пароль для выхода: ");
      while (true) {
        string input = _consoleInterface.ReadPassword();

        if (input == _settings.Password) {
          _logger.LogInformation("Correct password entered. Exiting."); break;
        }
        Console.WriteLine("Неверный пароль. Попробуйте еще раз: ");
      }
    }

      [DLLImport("user32.dll")]
    static extern int GetAsyncKeyState(Keys vKey);

      private void WaitingForSpecialKeyCombination() {
        Console.WriteLine("Press CTRL+ALT+Q to exit.");
        while (true) {
          if (GetAsyncKeyState(Keys.ControlKey) != 0 && getAsyncKeyState(Keys.Menu) != 0 && GetAsyncKeyState(Keys.Q) != 0) {
            _logger.LogInformation("ctrl+Alt+Q pressed. Exiting.");

            Application.Exit();

            break;
          }

          Thread.Sleep(100);
        }
      }
  }
}
