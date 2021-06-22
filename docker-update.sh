#!/bin/bash
echo Pull docker image
docker pull ghcr.io/fardragi/kurosawadia:latest
echo Stop container kurosawa-bot
docker stop kurosawa-bot
echo Remove container kurosawa-bot
docker rm kurosawa-bot
echo Create container kurosawa-bot
docker create --name kurosawa-bot --env-file .env --network Kurosawa ghcr.io/fardragi/kurosawadia
echo Start container kurosawa-bot
docker start kurosawa-bot
