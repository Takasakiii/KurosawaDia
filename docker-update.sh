#!/bin/bash
docker pull ghcr.io/fardragi/kurosawadia:latest
docker stop kurosawa-bot
docker rm kurosawa-bot
docker create --name kurosawa-bot --env-file .env --network Kurosawa ghcr.io/fardragi/kurosawadia
docker start kurosawa-bot
