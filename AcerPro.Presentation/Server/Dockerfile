#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AcerPro.Presentation/Server/AcerPro.Presentation.Server.csproj", "AcerPro.Presentation/Server/"]
COPY ["AcerPro.Application/AcerPro.Application.csproj", "AcerPro.Application/"]
COPY ["AcerPro.Persistence/AcerPro.Persistence.csproj", "AcerPro.Persistence/"]
COPY ["AcerPro.Common/AcerPro.Common.csproj", "AcerPro.Common/"]
COPY ["AcerPro.Domain/AcerPro.Domain.csproj", "AcerPro.Domain/"]
COPY ["Framework/Framework.csproj", "Framework/"]
COPY ["AcerPro.Presentation/Client/AcerPro.Presentation.Client.csproj", "AcerPro.Presentation/Client/"]
RUN dotnet restore "AcerPro.Presentation/Server/AcerPro.Presentation.Server.csproj"
COPY . .
WORKDIR "/src/AcerPro.Presentation/Server"
RUN dotnet build "AcerPro.Presentation.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AcerPro.Presentation.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AcerPro.Presentation.Server.dll"]