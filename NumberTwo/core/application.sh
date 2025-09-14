#!/bin/bash

#Определяем операционную систему
source "./platform/os_detect.sh"

#Загружаем конфигурацию
source "./core/configuration.sh"

#Подключаем функции для ОС
if [ "$OS" == "Windows" ]; then
  source "./platform/windows.sh"
elif [ "$OS" == "Linux" ]; then
  source "./platform/linux.sh"
else
  echo "Unsupported operating system: $OS"
  exit 1
fi

#Отображаем сообщение
source "./ui/message.sh"
display_message "$MESSAGE"

#Пытаемся заблокировть клавиатуру
if [ "$OS" == "Linux" ]; then
  source "./input/keyboard_blocker.sh"
  block_keyboard
fi

#Ожидаем условия выхода
source "./core/exit_condirion.sh"
wait_for_exit

#Очистка (только для Linux)
if [ "$OS" =="Linux" ]; then
  unblock_keyboard
fi

echo "Ethical Exploit stopped"
exit 0
