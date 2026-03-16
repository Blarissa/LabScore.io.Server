# Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["LabScore.io.Server.csproj", "./"]
RUN dotnet restore "./LabScore.io.Server.csproj"

COPY . .
RUN dotnet publish "./LabScore.io.Server.csproj" -c Release -o /app/publish --no-restore /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "LabScore.io.Server.dll"]