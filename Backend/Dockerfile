FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src

COPY *.sln .
COPY KebabQuest.WebApi/*.csproj ./KebabQuest.WebApi/
COPY KebabQuest.Services/*.csproj ./KebabQuest.Services/
COPY KebabQuest.Data/*.csproj ./KebabQuest.Data/

RUN dotnet restore

COPY . .

WORKDIR /src/KebabQuest.WebApi/
RUN dotnet build "KebabQuest.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "KebabQuest.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KebabQuest.WebApi.dll"]
