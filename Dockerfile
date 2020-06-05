  # https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /s/l

# copy csproj and restore as distinct layers
COPY /*.sln .
COPY LoveQuesto/. ./LoveQuesto/
COPY KAT/. ./KAT/
COPY LoveQuestoTests/. ./LoveQuestoTests/
COPY TelegramBotBase/. ./TelegramBotBase/

RUN dotnet publish -c release -o /app 


COPY wait-for-it.sh /app

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app
COPY --from=build /app ./
RUN chmod 777 wait-for-it.sh
ENTRYPOINT ["dotnet", "LoveQuesto.dll"]