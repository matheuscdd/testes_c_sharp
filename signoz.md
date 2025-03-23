## Baixa repositório
git clone -b main https://github.com/SigNoz/signoz.git


## Ir para a pasta
 cd signoz/deploy/docker

## Inicia docker compose
docker compose up -d --remove-orphans

## Conectar as redes
docker network create signoz-network2
docker network connect signoz-network2 signoz-otel-collector
docker network connect signoz-network2 dev-asp-1

## Disponível
http://localhost:8080/