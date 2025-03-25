# Criar
dotnet new webapi -o api

# Entra na pasta
cd api

# Carrega variáveis de ambiente
source .env

# Hot reload
dotnet watch run

# Criação da pasta Models
mkdir Models

# Criação dos arquivos de model

https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver16&tabs=cli&pivots=cs1-bash

# Instalar o Entity Framework para SQL Server
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0

# Instalar outras dependências do Entity Framework
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0

# Instalar dependências de manipulação de Json
dotnet add package Newtonsoft.Json --version 13.0.3
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 8.0.0

# Instalar dependências para JWT
dotnet add package Microsoft.Extensions.Identity.Core --version 8.0.0
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 8.0.0
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.0

# Cria o arquivo de manifesto
dotnet new tool-manifest

# Adiciona o Entity Framework ao cli
dotnet tool install --local dotnet-ef

# Depois cria a pasta Data com o arquivo ApplicationDBContext e faz alterações no Program.cs

# Comandos para gerenciar as migrações
Não pode ter o hot reload ativo para rodar migrações

## Cria os arquivos de migrações
dotnet ef migrations add init

## Aplica as migrações
dotnet ef database update

## Aplica migrações pelo docker
docker exec -it project-asp-migrater-1 sh -c "dotnet tool restore && dotnet ef database update --verbose"

# SQL Server
docker run \
   -e "ACCEPT_EULA=Y" \
   -e "MSSQL_SA_PASSWORD=das@3212ASD5d465as4da65" \
   -e "MSSQL_TLS_ENFORCE=0" \
   -p 1433:1433 --name sql1 --hostname sql1 \
   -d \
   mcr.microsoft.com/mssql/server:2022-latest

# Linha de comando para acessar SQL Server
find / -name sqlcmd 2>/dev/null

/opt/mssql-tools18/bin/sqlcmd -S 'localhost,1433' -U sa -P 'das@3212ASD5d465as4da65' -Q "SELECT name FROM sys.databases;" -C

/opt/mssql-tools18/bin/sqlcmd -S 'localhost,1433' -U sa -P 'das@3212ASD5d465as4da65' -Q "CREATE DATABASE finshark;" -C

# Passo a passo
Cria model > Registra no DbContext > Cria Dto > Cria Map > Cria InterfaceRepo > Cria Repo > Adicionar no Program.cs > Cria Controller

# Dos unix
dos2unix script.sh

# Docker
## Rodar migrações dev
docker compose -f docker-compose.dev.yml exec -T migrater dotnet tool restore
docker compose -f docker-compose.dev.yml exec -T migrater dotnet ef database update

## Rodar migrações prod
docker compose -f docker-compose.prod.yml exec -T migrater dotnet tool restore
docker compose -f docker-compose.prod.yml exec -T migrater dotnet ef database update

# Criar Pasta com classes
dotnet new classlib -n Application
dotnet new classlib -n Domain
dotnet new classlib -n Repository

# Novo modelo onion
## Criar migrações precisa estar na raiz do projeto (src)
dotnet ef migrations add Init -o Migrations --project Repository --startup-project Api

## Executa migrações na raiz (src)
dotnet ef database update --project Repository --startup-project Api

## Rodar (src)
dotnet watch run --project Api

## Build (src)
dotnet build Api