﻿FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TaskMicroservice/TaskMicroservice.csproj", "TaskMicroservice/"]
RUN dotnet restore "TaskMicroservice/TaskMicroservice.csproj"
COPY . .
WORKDIR "/src/TaskMicroservice"
RUN dotnet build "./TaskMicroservice.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TaskMicroservice.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

USER $APP_UID
WORKDIR /app
COPY --from=publish /app/publish .

CMD ["dotnet", "TaskMicroservice.dll"]
