﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./socialNetworkApp.csproj", "socialNetworkApp/"]
RUN dotnet restore "socialNetworkApp/socialNetworkApp.csproj"
COPY . ./socialNetworkApp
WORKDIR "/src/socialNetworkApp"
RUN dotnet build "socialNetworkApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "socialNetworkApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "socialNetworkApp.dll"]
