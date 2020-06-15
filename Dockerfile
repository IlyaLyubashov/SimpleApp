  # https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /s/l

# copy csproj and restore as distinct layers
COPY /*.sln .
COPY FakeCMS/. ./FakeCMS/
COPY FakeCMS.DAL/. ./FakeCMS.DAL/
COPY FakeCMS.BL/. ./FakeCMS.BL/
COPY FakeCMS.Shared/. ./FakeCMS.Shared/

RUN apt-get update -yq \
    && apt-get install curl gnupg make chromium -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq

RUN dotnet publish -c release -o /app 


COPY wait-for-it.sh /app

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app
COPY --from=build /app ./
RUN chmod 777 wait-for-it.sh
ENTRYPOINT ["dotnet", "FakeCMS.dll"]