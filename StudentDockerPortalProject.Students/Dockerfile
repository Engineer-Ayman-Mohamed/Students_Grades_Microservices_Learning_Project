# ═══════════════════════════════════════════════════
# Students Microservice — Multi-stage Docker Build
# ═══════════════════════════════════════════════════
# Stage 1: Build — restores dependencies and publishes a Release build
# Stage 2: Runtime — copies published output into a slim ASP.NET image

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app
COPY ["StudentDockerPortalProject.Students.csproj", "./"]
RUN dotnet restore "StudentDockerPortalProject.Students.csproj"
COPY . .
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "StudentDockerPortalProject.Students.dll"]