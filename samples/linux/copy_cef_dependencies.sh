#!/bin/sh

echo Copying spectre dependencies ...

config=$1
chromium_src=$CHROMIUM_SRC
target_dir=$PWD

echo "Copying cef binaries (mandatory) ..."
cp $chromium_src/out/$config/lib.target/libcef.so $target_dir/libcef.so

echo "Copying codecs (optional) ..."
cp $chromium_src/out/$config/libffmpegsumo.so $target_dir/libffmpegsumo.so

echo "Copying pak files (partially optional)..."
cp $chromium_src/out/$config/*.pak $target_dir/

echo "Copying locales (partially optional) ..."
mkdir -p $target_dir/locales
cp $chromium_src/out/$config/locales/* $target_dir/locales/

echo Finished copying dependencies.


