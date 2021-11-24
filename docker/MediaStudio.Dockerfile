#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MediaStudio.Services/MediaStudio/MediaStudio/MediaStudio.csproj", "MediaStudio.Services/MediaStudio/MediaStudio/"]
COPY ["MediaStudio.Services/DBContext/DBContext.csproj", "MediaStudio.Services/DBContext/"]
COPY ["MediaStudio.Services/MediaStudio/MediaStudio.Service/MediaStudio.Service.csproj", "MediaStudio.Services/MediaStudio/MediaStudio.Service/"]
RUN dotnet restore "MediaStudio.Services/MediaStudio/MediaStudio/MediaStudio.csproj"
COPY . .
WORKDIR "/src/MediaStudio.Services/MediaStudio/MediaStudio"
RUN dotnet build "MediaStudio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MediaStudio.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaStudio.dll"]