#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["./src/Plurish.Game.Api/Plurish.Game.Api.csproj", "./Plurish.Game.Api/"]
COPY ["./src/Plurish.Game.Application/Plurish.Game.Application.csproj", "./Plurish.Game.Application/"]
COPY ["./src/Plurish.Game.Domain/Plurish.Game.Domain.csproj", "./Plurish.Game.Domain/"]
COPY ["./src/Plurish.Game.Infra/Plurish.Game.Infra.csproj", "./Plurish.Game.Infra/"]
COPY ["./src/Plurish.Common/Plurish.Common.csproj", "./Plurish.Common/"]
COPY ./Directory.Build.props ./
COPY ./.editorconfig ./

RUN dotnet restore "./Plurish.Game.Api/Plurish.Game.Api.csproj"

COPY ./src/ ./

WORKDIR "/src/Plurish.Game.Api"

RUN dotnet build "./Plurish.Game.Api.csproj" -c Release -o /app/build

# TODO: adicionar dotnet test

FROM build AS publish

RUN dotnet publish "./Plurish.Game.Api.csproj" \
    -c Release \
    -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

EXPOSE 8080
EXPOSE 8081

USER app
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Plurish.Game.Api.dll"]