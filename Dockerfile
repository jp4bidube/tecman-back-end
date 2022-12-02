#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https -t
RUN dotnet dev-certs https --check
RUN dotnet dev-certs https --trust

COPY ["Tecman.csproj", "Tecman/"]
RUN dotnet restore "Tecman.csproj"
COPY . .
WORKDIR "./"
RUN dotnet build "Tecman.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tecman.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tecman.dll"]
