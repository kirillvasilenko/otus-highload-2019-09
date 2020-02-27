#!/bin/bash

buildService () {
  SVC_NAME=$1
  PRJ_ROOT=$2

  cd $PRJ_ROOT
  rm -rf "./.next"

  npm ci
  npm run build
  npm prune --production

  docker build  -t "hl/$SVC_NAME" -f "./Dockerfile" "."
}

buildService "frontend" ".."









