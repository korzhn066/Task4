#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Task4/Task4.csproj", "Task4/"]
RUN dotnet restore "Task4/Task4.csproj"
COPY . .
WORKDIR "/src/Task4"
RUN dotnet build "Task4.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Task4.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Task4.dll"]