FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.7-alpine3.12 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.401-alpine3.12 AS publish
WORKDIR /src
COPY . .
WORKDIR "/src/."
RUN dotnet publish "Codit.Exercises.DevSecOps.API/Codit.Exercises.DevSecOps.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Codit.Exercises.DevSecOps.API.dll"]
