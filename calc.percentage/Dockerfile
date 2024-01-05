#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["calc.percentage/calc.percentage.csproj", "calc.percentage/"]
RUN dotnet restore "calc.percentage/calc.percentage.csproj"
COPY . .
WORKDIR "/src/calc.percentage"
RUN dotnet build "calc.percentage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "calc.percentage.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "calc.percentage.dll"]