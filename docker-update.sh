#!/bin/bash
docker rm kurosawa-bot
docker pull ghcr.io/fardragi/kurosawadia:latest
docker create --name kurosawa-bot --env-file .env --network Kurosawa ghcr.io/fardragi/kurosawadia
docker run kurosawa-bot
