#!/bin/bash 
 
#CURRENT_TIME=$(date +%s) 


buildService () {
  SVC_NAME=$1
  PRJ_ROOT=$2
  
  rm -r "$PRJ_ROOT/bin"
  rm -r "$PRJ_ROOT/obj"
  dotnet publish -c Release "$PRJ_ROOT"
  
  docker build  -t "hl/$SVC_NAME" -f "$PRJ_ROOT/Dockerfile" "$PRJ_ROOT"
}

dotnet restore ..

buildService "socialnetwork" "../1.SocialNetwork/SocialNetwork.AspNet"
buildService "socialnetwork-db-migrator" "../1.SocialNetwork/SocialNetwork.Migrator.MySql"

docker build -t hl/nginx -f nginx/Dockerfile nginx









