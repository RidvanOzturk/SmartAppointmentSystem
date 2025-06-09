FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Render ortamı için: ASPNETCORE_URLS port ayarı
ENV ASPNETCORE_URLS=http://+:$PORT

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["SmartAppointmentSystem.Api/SmartAppointmentSystem.Api.csproj", "SmartAppointmentSystem.Api/"]
COPY ["SmartAppointmentSystem.Business/SmartAppointmentSystem.Business.csproj", "SmartAppointmentSystem.Business/"]
COPY ["SmartAppointmentSystem.Data/SmartAppointmentSystem.Data.csproj", "SmartAppointmentSystem.Data/"]
RUN dotnet restore "SmartAppointmentSystem.Api/SmartAppointmentSystem.Api.csproj"
COPY . .
WORKDIR "/src/SmartAppointmentSystem.Api"
RUN dotnet build "SmartAppointmentSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartAppointmentSystem.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartAppointmentSystem.Api.dll"]
