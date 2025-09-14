using System;

namespace EthicalExploit.UI {
  public class ConsoleInterface {
    public string ReadPassword() {
      string password = "";
      ConsoleKeyInfo key;

      do {
        key = Console.ReadKey(true);

        if(key.key == ConsoleKey.Backspace && password.Length > 0) {
          password = password.Substring(0, (password.Length - 1)); 
          Console.Write("\b \b");
        }

        else if (key.key != ConsoleKey.Backspace) {
          password += key.KeyChar;
          Console.Write("*");
        }
      }

      while (key.key != ConsoleKey.Enter);

      Console.WriteLine();

      return password;
    }
  }
}
