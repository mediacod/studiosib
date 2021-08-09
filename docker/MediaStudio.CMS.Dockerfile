#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 8082

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MediaStudio.Services/MediaStudio.CMS/MediaStudio.CMS/MediaStudio.CMS.csproj", "MediaStudio.Services/MediaStudio.CMS/MediaStudio.CMS/"]
COPY ["MediaStudio.Services/DBContext/DBContext.csproj", "MediaStudio.Services/DBContext/"]
COPY ["MediaStudio.Services/MediaStudio.CMS/MediaStudio.CMS.Service/MediaStudio.CMS.Service.csproj", "MediaStudio.Services/MediaStudio.CMS/MediaStudio.CMS.Service/"]
RUN dotnet restore "MediaStudio.Services/MediaStudio.CMS/MediaStudio.CMS/MediaStudio.CMS.csproj"
COPY . .
WORKDIR "/src/MediaStudio.Services/MediaStudio.CMS/MediaStudio.CMS"
RUN dotnet build "MediaStudio.CMS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MediaStudio.CMS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaStudio.CMS.dll"]