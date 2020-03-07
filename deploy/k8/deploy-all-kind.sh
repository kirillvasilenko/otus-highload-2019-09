#!/usr/bin/env bash

docker pull docker.pkg.github.com/kirillamurskiy/otus-highload-2019-09/socialnetwork-users-generator:latest
kind load docker-image docker.pkg.github.com/kirillamurskiy/otus-highload-2019-09/socialnetwork-users-generator:latest

docker pull docker.pkg.github.com/kirillamurskiy/otus-highload-2019-09/socialnetwork:latest
kind load docker-image docker.pkg.github.com/kirillamurskiy/otus-highload-2019-09/socialnetwork:latest

docker pull docker.pkg.github.com/kirillamurskiy/otus-highload-2019-09/socialnetwork-db-migrator:latest
kind load docker-image docker.pkg.github.com/kirillamurskiy/otus-highload-2019-09/socialnetwork-db-migrator:latest

docker pull docker.pkg.github.com/kirillamurskiy/otus-highload-2019-09/frontend:latest
kind load docker-image docker.pkg.github.com/kirillamurskiy/otus-highload-2019-09/frontend:latest

kubectl create configmap nginx-config --from-file=nginx/default.conf --from-file =nginx/nginx.conf

kubectl apply -f mysql-service.yaml
kubectl apply -f nginx-service.yaml
kubectl apply -f socialnetwork-service.yaml
kubectl apply -f frontend-service.yaml

kubectl apply -f mysql-deployment.yaml
kubectl apply -f nginx-deployment.yaml
kubectl apply -f socialnetwork-deployment.yaml
kubectl apply -f frontend-deployment.yaml