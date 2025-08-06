FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /source
COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c release -o /app --self-contained false --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT [ "dotnet", "performancer.dll" ]