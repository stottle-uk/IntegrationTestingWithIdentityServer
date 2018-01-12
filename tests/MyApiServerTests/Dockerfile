FROM microsoft/dotnet:2.0-sdk

WORKDIR ./app

COPY ./tests/MyApiServerTests/MyApiServerTests.csproj ./tests/MyApiServerTests/
COPY ./src/MyApiServer/MyApiServer.csproj ./src/MyApiServer/

RUN dotnet restore ./tests/MyApiServerTests/MyApiServerTests.csproj

COPY . .

ENV IDENTITY_SERVER_AUTHORITY=http://localhost:8002
ENV IDENTITY_SERVER_AUDIENCE=http://localhost:8002/resources
ENV IDENTITY_SERVER_REQUIREHTTPSMETADATA=false

ENTRYPOINT ["dotnet", "test", "./tests/MyApiServerTests/MyApiServerTests.csproj"]