FROM mcr.microsoft.com/mssql/server:2022-latest

COPY docker-entrypoint.sh .

USER root
RUN apt update && apt install netcat -y

ENTRYPOINT ./docker-entrypoint.sh & /opt/mssql/bin/sqlservr
