#!/usr/bin/env bash

kind create cluster --config=kind-config.yaml #--name=my-cluster

kubectl cluster-info --context kind-kind