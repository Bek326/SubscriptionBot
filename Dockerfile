# Use the official .NET SDK as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj files and restore as distinct layers
COPY *.sln ./
COPY SubscriptionBot.API/*.csproj ./SubscriptionBot.API/
COPY SubscriptionBot.Application/*.csproj ./SubscriptionBot.Application/
COPY SubscriptionBot.Domain/*.csproj ./SubscriptionBot.Domain/
COPY SubscriptionBot.Infrastructure/*.csproj ./SubscriptionBot.Infrastructure/
COPY SubscriptionBot.Tests/*.csproj ./SubscriptionBot.Tests/

RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the project
RUN dotnet build -c Release --no-restore

# Publish the project
RUN dotnet publish SubscriptionBot.API/SubscriptionBot.API.csproj -c Release -o /out --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /out .

ENTRYPOINT ["dotnet", "SubscriptionBot.API.dll"]
