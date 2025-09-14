#!/bin/bash

# Блокировка клавиатуры с помощью xinput
block_keyboard() {
  # Получаем ID клавиатуры
  KEYBOARD_ID=$(xinput list | grep "keyboard" | grep -o "id=[0-9]*" | grep -o "[0-9]*")

  if [ -z "$KEYBOARD_ID" ]; then
    echo "Keyboard not found using xinput."
    return
  fi

  # Блокируем клавиатуру
  xinput disable "$KEYBOARD_ID"
  echo "Keyboard disabled (xinput)."
}

# Разблокировка клавиатуры с помощью xinput
unblock_keyboard() {
  # Получаем ID клавиатуры (проверяем, что она еще существует)
  KEYBOARD_ID=$(xinput list | grep "keyboard" | grep -o "id=[0-9]*" | grep -o "[0-9]*")

  if [ -z "$KEYBOARD_ID" ]; then
    echo "Keyboard not found using xinput."
    return
  fi

  xinput enable "$KEYBOARD_ID"
  echo "Keyboard enabled (xinput)."
}
