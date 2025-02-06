FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["SmartAppointmentSystem.Api/SmartAppointmentSystem.Api.csproj", "SmartAppointmentSystem.Api/"]
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
