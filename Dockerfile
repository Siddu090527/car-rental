FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CarRental.Api/CarRental.Api.csproj", "CarRental.Api/"]
COPY ["CarRental.Tests/CarRental.Tests.csproj", "CarRental.Tests/"]
RUN dotnet restore "CarRental.Api/CarRental.Api.csproj"
COPY . .
WORKDIR "/src/CarRental.Api"
RUN dotnet publish "CarRental.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CarRental.Api.dll"]
