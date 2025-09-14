using System;
using System.Diagnostics;

namespace EthicalExploit.Helpers {
  public static class ProcessHelper {
    public static string GetProcessName() {

      return Process.GetCurrentProcess().ProcessName;
    }
  }
}
