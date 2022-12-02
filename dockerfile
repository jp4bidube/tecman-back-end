FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./Tecman.sln .
COPY ./Tecman.csproj ./Tecman/
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish --no-restore -c Release -o out ./Tecman

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["./Tecman"]