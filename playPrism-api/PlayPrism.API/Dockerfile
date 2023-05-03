#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PlayPrism.API/PlayPrism.API.csproj", "PlayPrism.API/"]
COPY ["PlayPrism.BLL/PlayPrism.BLL.csproj", "PlayPrism.BLL/"]
COPY ["PlayPrism.BLL.Abstractions/PlayPrism.BLL.Abstractions.csproj", "PlayPrism.BLL.Abstractions/"]
COPY ["PlayPrism.Core/PlayPrism.Core.csproj", "PlayPrism.Core/"]
COPY ["PlayPrism.DAL/PlayPrism.DAL.csproj", "PlayPrism.DAL/"]
COPY ["PlayPrism.DAL.Abstractions/PlayPrism.DAL.Abstractions.csproj", "PlayPrism.DAL.Abstractions/"]
COPY ["PlayPrism.Contracts/PlayPrism.Contracts.csproj", "PlayPrism.Contracts/"]
RUN dotnet restore "PlayPrism.API/PlayPrism.API.csproj"
COPY . .
WORKDIR "/src/PlayPrism.API"
RUN dotnet build "PlayPrism.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlayPrism.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayPrism.API.dll"]