#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/SGM.Autenticacao.Api/SGM.Autenticacao.Api.csproj", "src/SGM.Autenticacao.Api/"]
COPY ["src/SGM.Autenticacao.Application/SGM.Autenticacao.Application.csproj", "src/SGM.Autenticacao.Application/"]
COPY ["src/SGM.Autenticacao.Repository/SGM.Autenticacao.Repository.csproj", "src/SGM.Autenticacao.Repository/"]
COPY ["src/SGM.Autenticacao.Domain/SGM.Autenticacao.Domain.csproj", "src/SGM.Autenticacao.Domain/"]

RUN dotnet restore "src/SGM.Autenticacao.Api/SGM.Autenticacao.Api.csproj"
COPY . .
WORKDIR "/src/src/SGM.Autenticacao.Api"
RUN dotnet build "SGM.Autenticacao.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGM.Autenticacao.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGM.Autenticacao.Api.dll"]