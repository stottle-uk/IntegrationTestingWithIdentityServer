#!/usr/bin/env bash

CURDIR=`pwd`

docker run --rm \
           -d \
           --name my-identity-server \
           -v "${CURDIR}/src/MyIdentityServer/:/app" \
           --workdir /app \
           microsoft/dotnet:2.0-sdk dotnet run --project ./MyIdentityServer.csproj

docker run --rm \
           --name my-api-server-tests \
           -v "${CURDIR}/:/build" \
           --workdir /build \
           --net container:my-identity-server \
           -e IDENTITY_SERVER_AUTHORITY=http://localhost:5000 \
           -e IDENTITY_SERVER_AUDIENCE=http://localhost:5000/resources \
           -e IDENTITY_SERVER_REQUIREHTTPSMETADATA=false \
           microsoft/dotnet:2.0-sdk dotnet test ./tests/MyApiServerTests/MyApiServerTests.csproj

docker stop my-identity-server 

docker volume ls -qf dangling=true