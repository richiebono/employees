#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["0-Test/Bono.Employees.Application.Tests/Bono.Employees.Application.Tests.csproj", "0-Test/Bono.Employees.Application.Tests/"]
RUN dotnet restore "0-Test/Bono.Employees.Application.Tests/Bono.Employees.Application.Tests.csproj"
COPY . .
WORKDIR "/src/0-Test/Bono.Employees.Application.Tests"
RUN dotnet build "Bono.Employees.Application.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bono.Employees.Application.Tests.csproj" -c Release -o /app/publish

RUN dotnet test "Bono.Employees.Application.Tests.csproj"

FROM base AS final
WORKDIR app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bono.Employees.Application.Tests.dll"]

