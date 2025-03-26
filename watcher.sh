#!/usr/bin/env sh

[ ! -d "signoz" ] && git clone -b main https://github.com/SigNoz/signoz.git
cd signoz/deploy/docker || exit

docker compose up -d --remove-orphans

docker network create signoz-network 1>/dev/null 2>&1
docker network connect signoz-network proj-prod-asp-1 1>/dev/null 2>&1
docker network connect signoz-network signoz-otel-collector  1>/dev/null 2>&1