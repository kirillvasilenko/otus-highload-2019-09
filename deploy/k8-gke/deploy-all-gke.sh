#!/usr/bin/env bash

kubectl create secret generic regcred \
    --from-file=.dockerconfigjson=/C:/Users/kir/.docker/config1.json \
    --type=kubernetes.io/dockerconfigjson

kubectl create configmap nginx-config \
    --from-file=default.conf=nginx/default.conf \
    --from-file=nginx.conf=nginx/nginx.conf

kubectl apply -f mysql-service.yaml
kubectl apply -f nginx-service.yaml
kubectl apply -f socialnetwork-service.yaml
kubectl apply -f frontend-service.yaml

kubectl apply -f mysql-deployment.yaml
kubectl apply -f nginx-deployment.yaml
kubectl apply -f socialnetwork-deployment.yaml
kubectl apply -f frontend-deployment.yaml