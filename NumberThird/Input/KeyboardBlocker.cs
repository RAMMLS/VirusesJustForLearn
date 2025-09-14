using System;
using System.Runtime.InteropServices;
using EthicalExploit.Logging;

namespace EthicalExploit.Input
{
    public class KeyboardBlocker : IInputBlocker
    {
        private readonly ILogger _logger;

        public KeyboardBlocker(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, HookProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr hookId, int code, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        private const int WH_KEYBOARD_LL = 13;

        private delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);
        private static IntPtr _hookID = IntPtr.Zero;

        private static IntPtr LowLevelKeyboardProc(int code, IntPtr wParam, IntPtr lParam)
        {
            return (IntPtr)1;
        }

        public void BlockInput()
        {
            _hookID = SetHook(LowLevelKeyboardProc);
            _logger.LogInformation("Keyboard blocked.");
        }

        public void UnblockInput()
        {
            UnhookWindowsHookEx(_hookID);
             _logger.LogInformation("Keyboard unblocked.");
        }

         private static IntPtr SetHook(HookProc proc)
        {
            using (System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (System.Diagnostics.ProcessModule currentModule = currentProcess.MainModule)
            {
                IntPtr moduleHandle = GetModuleHandle(currentModule.ModuleName);
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, moduleHandle, 0);
            }
        }

    }
}

