#!/bin/sh

readonly WORK_DIR=$(cd $(dirname $0); pwd)
readonly SOURCE_PATH=$WORK_DIR/AndroidProject/unisharper-core/build/outputs/aar
readonly TARGET_PATH=$WORK_DIR/Assets/UniSharper.Core/Plugins/Android
readonly SOURCE_FILE_NAME="unisharper-core-debug.aar"
readonly NEW_FILE_NAME="unisharper-core.aar"
echo "Synchronizing Android plugin of UniSharper.Core..."

mkdir -p $TARGET_PATH
rm -f $TARGET_PATH/$NEW_FILE_NAME
cp -rfp $SOURCE_PATH/$SOURCE_FILE_NAME $TARGET_PATH/$NEW_FILE_NAME