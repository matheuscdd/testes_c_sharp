#!/usr/bin/env bash

readonly id=$(docker ps --no-trunc | grep migrater | cut -d ' ' -f 1)
docker exec -it "$id" sh -c "dotnet tool restore && dotnet ef database update --verbose"
