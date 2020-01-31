#!/bin/bash 
 
#CURRENT_TIME=$(date +%s) 


buildService () {
  SVC_NAME=$1
  PRJ_ROOT=$2
  
  rm -r "$PRJ_ROOT/bin"
  rm -r "$PRJ_ROOT/obj"
  dotnet publish -c Release "$PRJ_ROOT"
  
  docker build  -t "social/$SVC_NAME" -f "$PRJ_ROOT/Dockerfile" "$PRJ_ROOT"
}

dotnet restore ..

buildService "auth" "../1.Auth/AuthService.AspNet"
buildService "users" "../2.Users/UsersService.AspNet"
buildService "users-db-migrator" "../2.Users/UsersService.Migrator.MySql"

docker build -t social/nginx -f nginx/Dockerfile nginx









