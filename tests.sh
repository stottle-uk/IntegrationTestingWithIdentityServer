#!/usr/bin/env bash

echo
echo ==================================================================
echo  Building and running Identity Server
echo ==================================================================
echo

docker build -f ./src/MyIdentityServer/Dockerfile -t myidentityserver .

docker run --rm -d -p 8002:8002 --name my-identity-server myidentityserver

echo
echo ==================================================================
echo  Building and running tests
echo ==================================================================
echo

docker build -f ./tests/MyApiServerTests/Dockerfile -t myapi_tests .

docker run --rm --net "host" --name my-api-server-tests myapi_tests

echo
echo ==================================================================
echo  Tidy up - stop Identity Server
echo ==================================================================
echo

docker stop my-identity-server
