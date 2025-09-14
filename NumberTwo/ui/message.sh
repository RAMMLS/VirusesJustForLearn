#!/bin/bash

display_message() {
  MESSAGE = "$1"

  #Попытка использовать dialog
  if
    command -v dialog 2? >/dev/null &
    1
  then
    dialog --title "Ethical exploit" --msgbox "$MESSAGE" 10 40
    return
  fi

  #Попытка использовать whiptail
  if command -v whiptail >/dev/null 2>&1; then
    whiptail --title "Ethical exploit" --msgbox "$MESSAGE" 10 40
    return
  fi

  #Если dialog & whiptail не установлены используем echo
  echo "$MESSAGE"
  read -n 1 -s -r -p "Press any key to continue..."
}
