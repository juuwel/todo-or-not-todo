FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

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

# Copy the wait-for-postgres.sh script to the final image
USER root
COPY wait-for-postgres.sh /app/wait-for-postgres.sh
RUN apt-get update && apt-get install -y netcat-openbsd dos2unix
RUN chmod +x /app/wait-for-postgres.sh && dos2unix wait-for-postgres.sh

USER $APP_UID
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["/app/wait-for-postgres.sh"]
CMD ["dotnet", "TaskMicroservice.dll"]
