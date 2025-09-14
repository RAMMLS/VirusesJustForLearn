using System.Diagnostics;

namespace EthicalExploit.AntiDebug {
  public static class DebuggerDetector {

    public static bool IsDebuggerAttached() {

      return Debugger.IsAttached;
    }
  }
}
