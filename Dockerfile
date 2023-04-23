FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/DotNetWebApi/DotNetWebApi.csproj", "webapi/DotNetWebApi/"]
COPY ["src/WebApi.Models/WebApi.Models.csproj", "webapi/WebApi.Models/"]
RUN dotnet restore "webapi/DotNetWebApi/DotNetWebApi.csproj"
COPY . .
WORKDIR "/src/webapi/DotNetWebApi"
RUN dotnet build "DotNetWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetWebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetWebApi.dll"]
