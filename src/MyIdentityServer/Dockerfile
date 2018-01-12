FROM microsoft/dotnet:2.0-sdk

WORKDIR /app

ENV ASPNETCORE_URLS=http://*:8002

EXPOSE 8002

COPY ./src/MyIdentityServer/MyIdentityServer.csproj .

RUN dotnet restore ./MyIdentityServer.csproj

COPY ./src/MyIdentityServer/ .

ENTRYPOINT [ "dotnet", "run" ]