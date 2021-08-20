#!/bin/bash
set -e
VERSION=0.1.1
REPO="525595969507.dkr.ecr.eu-central-1.amazonaws.com/qwan/vendingrefact-cpp"

command=$1
case $command in
  push)
    docker push $REPO:$VERSION
    ;;
  *)
    docker build . -t $REPO:$VERSION
    ;;
esac
