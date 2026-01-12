FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ../poc.ai.ollama.generative.api/*.csproj ./poc.ai.ollama.generative.api/
RUN dotnet restore ./poc.ai.ollama.generative.api/poc.ai.ollama.generative.api.csproj

COPY ../poc.ai.ollama.generative.api/. ./poc.ai.ollama.generative.api/
WORKDIR /src/poc.ai.ollama.generative.api
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "poc.ai.ollama.generative.api.dll"]
