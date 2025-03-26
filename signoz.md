## Baixa repositório
git clone -b main https://github.com/SigNoz/signoz.git


## Ir para a pasta
 cd signoz/deploy/docker

## Inicia docker compose
docker compose up -d --remove-orphans

## Conectar as redes
docker network create signoz-network
docker network connect signoz-network signoz-otel-collector
docker network connect signoz-network proj-prod-asp-1

## Disponível
http://localhost:8000/

# Instalação
dotnet add package OpenTelemetry --version 1.11.2
dotnet add package OpenTelemetry.AutoInstrumentation --version 1.11.0
dotnet add package OpenTelemetry.Exporter.Console --version 1.11.2
dotnet add package OpenTelemetry.Exporter.OpenTelemetryProtocol --version 1.11.2
dotnet add package OpenTelemetry.Extensions.Hosting --version 1.11.2
dotnet add package OpenTelemetry.Instrumentation.AspNetCore --version 1.11.1
dotnet add package OpenTelemetry.Instrumentation.Http --version 1.11.1
dotnet add package OpenTelemetry.Instrumentation.Runtime --version 1.11.1



