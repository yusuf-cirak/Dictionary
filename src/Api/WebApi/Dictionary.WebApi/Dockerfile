#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Api/WebApi/Dictionary.WebApi/Dictionary.WebApi.csproj", "src/Api/WebApi/Dictionary.WebApi/"]
COPY ["src/Api/Infrastructure/Dictionary.Persistence/Dictionary.Persistence.csproj", "src/Api/Infrastructure/Dictionary.Persistence/"]
COPY ["src/Api/Core/Dictionary.Application/Dictionary.Application.csproj", "src/Api/Core/Dictionary.Application/"]
COPY ["src/Api/Core/Dictionary.Domain/Dictionary.Domain.csproj", "src/Api/Core/Dictionary.Domain/"]
COPY ["src/Common/Dictionary.Common/Dictionary.Common.csproj", "src/Common/Dictionary.Common/"]
RUN dotnet restore "src/Api/WebApi/Dictionary.WebApi/Dictionary.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Api/WebApi/Dictionary.WebApi"
RUN dotnet build "Dictionary.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dictionary.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dictionary.WebApi.dll"]