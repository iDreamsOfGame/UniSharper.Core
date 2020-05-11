#!/bin/sh

readonly WORK_DIR=$(cd $(dirname $0); pwd)
cd $WORK_DIR
openupm add io.github.idreamsofgame.resharp.core
openupm add jillejr.newtonsoft.json-for-unity