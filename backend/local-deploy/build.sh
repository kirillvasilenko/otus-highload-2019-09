#!/bin/bash 
 
CURRENT_TIME=$(date +%s)

buildService () {
  SVC_NAME=$1
  PRJ_ROOT=$2
  
  dotnet clean -c Release "$PRJ_ROOT"
  dotnet publish -c Release "$PRJ_ROOT"
  
  docker build -t "social/$SVC_NAME:$CURRENT_TIME" -t "social/$SVC_NAME:latest" -f "$PRJ_ROOT/Dockerfile" "$PRJ_ROOT"
}

#cd ..

dotnet restore

#buildService "auth" "../1.Auth/AuthService.AspNet"
#buildService "users" "../2.Users/UsersService.AspNet"
buildService "users-db-migrator" "../2.Users/UsersService.Repo.MySql.Migrator"









