#!/usr/bin/env sh

set -e

envsubst '${DOMAIN},${STAGING_URL}' < "/etc/nginx/conf.d/default.conf.template" > /etc/nginx/nginx.conf

exec "$@"