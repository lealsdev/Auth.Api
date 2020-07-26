# ---- Run Build ----
# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
COPY ./src /app/
WORKDIR /app/Auth.Api/
RUN dotnet restore
RUN dotnet publish -c release -o out

# ---- Run App ----
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as release
WORKDIR /app
COPY --from=build /app/Auth.Api/out .
ENTRYPOINT ["dotnet", "Auth.Api.dll"]
