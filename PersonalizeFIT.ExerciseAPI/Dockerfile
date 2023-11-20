#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PersonalizeFIT.ExerciseAPI/PersonalizeFIT.ExerciseAPI.csproj", "PersonalizeFIT.ExerciseAPI/"]
COPY ["ExerciseAPI.Business/ExerciseAPI.Business.csproj", "ExerciseAPI.Business/"]
COPY ["ExerciseAPI.Connectors/ExerciseAPI.Connectors.csproj", "ExerciseAPI.Connectors/"]
COPY ["ExerciseAPI.Entity/ExerciseAPI.Entity.csproj", "ExerciseAPI.Entity/"]
RUN dotnet restore "PersonalizeFIT.ExerciseAPI/PersonalizeFIT.ExerciseAPI.csproj"
COPY . .
WORKDIR "/src/PersonalizeFIT.ExerciseAPI"
RUN dotnet build "PersonalizeFIT.ExerciseAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonalizeFIT.ExerciseAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonalizeFIT.ExerciseAPI.dll"]