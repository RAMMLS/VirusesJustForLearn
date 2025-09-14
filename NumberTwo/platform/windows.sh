#!/bin/bash

# Пустышки для Windows (блокировка клавиатуры очень сложна)
block_keyboard() {
  echo "Keyboard blocking not implemented for Windows."
  # Можно попробовать запустить PowerShell скрипт, но это сложно
  # powershell -ExecutionPolicy Bypass -File block_keyboard.ps1
}

unblock_keyboard() {
  echo "Keyboard unblocking not implemented for Windows."
}

#Пример запуска powershell (нужно сначала создать .ps1 скрипт)
#run_powershell_script() {
#    powershell -ExecutionPolicy Bypass -File "$1"
#}
