#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Projections/Dictionary.VoteService/Dictionary.VoteService.csproj", "src/Projections/Dictionary.VoteService/"]
COPY ["src/Common/Dictionary.Common/Dictionary.Common.csproj", "src/Common/Dictionary.Common/"]
RUN dotnet restore "src/Projections/Dictionary.VoteService/Dictionary.VoteService.csproj"
COPY . .
WORKDIR "/src/src/Projections/Dictionary.VoteService"
RUN dotnet build "Dictionary.VoteService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dictionary.VoteService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dictionary.VoteService.dll"]