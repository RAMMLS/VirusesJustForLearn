using System;

namespace EthicalExploit.Logging {
  public class Logger : ILogger {
    public voidLogDebug(string message) {
      Console.WriteLine($"[Debug] {DateTime.Now}: {message}");
    }

    public void LogInformation(string message) {
      Console.WriteLine($"[Info] {DateTime.Now}:{message}");
    }

    public void LogWarning(string message) {
      Console.WriteLine($"[Warning] {DateTime.Now}:{message}");
    }

    public void LogError(string message) {
      Console.WriteLine($"[Error] {DateTime.Now}:{message}");
    }
  }
}
