FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=5001

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["app/Api/Api.csproj", "app/Api/"]
COPY ["app/Application/Application.csproj", "app/Application/"]
COPY ["app/Domain/Domain.csproj", "app/Domain/"]
COPY ["app/Infra/Infra.csproj", "app/Infra/"]
RUN dotnet restore "app/Api/Api.csproj"
COPY . .
WORKDIR "/src/app/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]