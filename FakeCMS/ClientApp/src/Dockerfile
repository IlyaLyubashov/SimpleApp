FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# Installing npm and node
RUN apt-get update -yq \
    && apt-get install curl gnupg make chromium -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
