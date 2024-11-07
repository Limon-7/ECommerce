FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#stage-1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

#restore
WORKDIR /src
COPY ["Services/Basket/Basket.API/Basket.API.csproj", "Services/Basket/Basket.API/"]
COPY ["Services/Basket/Basket.Application/Basket.Application.csproj", "Services/Basket/Basket.Application/"]
COPY ["Services/Basket/Basket.Domain/Basket.Domain.csproj", "Services/Basket/Basket.Domain/"]
COPY ["Services/Basket/Basket.Infrastructure/Basket.Infrastructure.csproj", "Services/Basket/Basket.Infrastructure/"]
COPY ["Infrastructures/EventBus.Service/EventBus.Service.csproj", "Infrastructures/EventBus.Service/"]

RUN dotnet restore "Services/Basket/Basket.API/Basket.API.csproj"

#build
COPY . .
WORKDIR "/src/Services/Basket/Basket.API"
RUN dotnet build "Basket.API.csproj" -c Release -o /app/build

#stage 2: Publish Stage
FROM build AS publish
RUN dotnet publish "Basket.API.csproj" -c Release -o /app/publish

# Step-3: Run Stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]
