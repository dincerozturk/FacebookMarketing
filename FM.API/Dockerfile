#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FM.API/FM.API.csproj", "FM.API/"]
COPY ["FM.Infrastructure.Shared/FM.Infrastructure.Shared.csproj", "FM.Infrastructure.Shared/"]
COPY ["FM.Infrastructure/FM.Infrastructure.csproj", "FM.Infrastructure/"]
COPY ["FM.Domain/FM.Domain.csproj", "FM.Domain/"]
COPY ["FM.Application/FM.Application.csproj", "FM.Application/"]
RUN dotnet restore "FM.API/FM.API.csproj"
COPY . .
WORKDIR "/src/FM.API"
RUN dotnet build "FM.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FM.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FM.API.dll"]