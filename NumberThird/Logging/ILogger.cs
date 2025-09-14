namespace EthicalExploit.Logging {
  public interface ILogger {
    void LogBedug(string message);
    void LoginInformation(string message);
    void LogWarning(string message);
    void LogError(string message);
  }
}
