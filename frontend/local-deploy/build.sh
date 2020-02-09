#!/bin/bash

buildService () {
  SVC_NAME=$1
  PRJ_ROOT=$2

  rm -rf "$PRJ_ROOT/.next"

  yarn
  yarn build
  yarn --production

  docker build  -t "hl/$SVC_NAME" -f "$PRJ_ROOT/Dockerfile" "$PRJ_ROOT"
}

buildService "frontend" ".."









