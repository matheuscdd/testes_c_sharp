# Criar
dotnet new webapi -o api

# Entra na pasta
cd api

# Hot reload
dotnet watch run

# Criação da pasta Models
mkdir Models

# Criação dos arquivos de model

https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver16&tabs=cli&pivots=cs1-bash

# SQL Server
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=das@3212ASD5d465as4da65" \
   -p 1433:1433 --name sql1 --hostname sql1 \
   -d \
   mcr.microsoft.com/mssql/server:2022-latest