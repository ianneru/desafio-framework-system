#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DesafioFrameworkSystem.Api/nuget.config", "DesafioFrameworkSystem.Api/"]
COPY ["DesafioFrameworkSystem.Api/DesafioFrameworkSystem.Api.csproj", "DesafioFrameworkSystem.Api/"]
COPY ["DesafioFrameworkSystem.Messaging.Send/DesafioFrameworkSystem.Messaging.Send.csproj", "DesafioFrameworkSystem.Messaging.Send/"]
COPY ["DesafioFrameworkSystem.Domain/DesafioFrameworkSystem.Domain.csproj", "DesafioFrameworkSystem.Domain/"]
COPY ["DesafioFrameworkSystem.Service/DesafioFrameworkSystem.Service.csproj", "DesafioFrameworkSystem.Service/"]
COPY ["DesafioFrameworkSystem.Application/DesafioFrameworkSystem.Application.csproj", "DesafioFrameworkSystem.Application/"]
RUN dotnet restore "DesafioFrameworkSystem.Api/DesafioFrameworkSystem.Api.csproj"
COPY . .
WORKDIR "/src/DesafioFrameworkSystem.Api"
RUN dotnet build "DesafioFrameworkSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DesafioFrameworkSystem.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioFrameworkSystem.Api.dll"]