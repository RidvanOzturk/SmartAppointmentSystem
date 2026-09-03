FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["SmartAppointmentSystem.Api/SmartAppointmentSystem.Api.csproj", "SmartAppointmentSystem.Api/"]
COPY ["SmartAppointmentSystem.Business/SmartAppointmentSystem.Business.csproj", "SmartAppointmentSystem.Business/"]
COPY ["SmartAppointmentSystem.Data/SmartAppointmentSystem.Data.csproj", "SmartAppointmentSystem.Data/"]
COPY ["SmartAppointmentSystem.Infrastructure/SmartAppointmentSystem.Infrastructure.csproj", "SmartAppointmentSystem.Infrastructure/"]
RUN dotnet restore "SmartAppointmentSystem.Api/SmartAppointmentSystem.Api.csproj"

COPY . .
RUN dotnet publish "SmartAppointmentSystem.Api/SmartAppointmentSystem.Api.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=10000
ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false
EXPOSE 10000
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SmartAppointmentSystem.Api.dll"]
