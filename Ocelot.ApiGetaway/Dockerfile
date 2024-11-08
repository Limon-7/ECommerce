FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ocelot.ApiGetaway/Ocelot.ApiGetaway.csproj", "Ocelot.ApiGetaway/"]
RUN dotnet restore "Ocelot.ApiGetaway/Ocelot.ApiGetaway.csproj"
COPY . .
WORKDIR "/src/Ocelot.ApiGetaway"
RUN dotnet build "Ocelot.ApiGetaway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ocelot.ApiGetaway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ocelot.ApiGetaway.dll"]
