#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/HavayarQuiz.API/HavayarQuiz.API.csproj", "src/HavayarQuiz.API/"]
RUN dotnet restore "src/HavayarQuiz.API/HavayarQuiz.API.csproj"
COPY . .
WORKDIR "/src/src/HavayarQuiz.API"
RUN dotnet build "HavayarQuiz.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HavayarQuiz.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HavayarQuiz.API.dll"]