#!/bin/bash
version="latest"

while getopts v: flag
do
    case "${flag}" in
        v) version=${OPTARG}
    esac
done

echo Pull docker image
docker pull ghcr.io/fardragi/kurosawadia:$version

echo Stop container kurosawa-bot
docker stop kurosawa-bot

echo Remove container kurosawa-bot
docker rm kurosawa-bot

echo Create container kurosawa-bot
docker create --name kurosawa-bot --env-file .env --network Kurosawa ghcr.io/fardragi/kurosawadia:$version

echo Start container kurosawa-bot
docker start kurosawa-bot
