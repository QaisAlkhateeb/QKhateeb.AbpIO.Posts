#!/bin/bash

if [[ ! -d certs ]]
then
    mkdir certs
    cd certs/
    if [[ ! -f localhost.pfx ]]
    then
        dotnet dev-certs https -v -ep localhost.pfx -p 036bd27a-4228-46d4-bf48-9e33555a4429 -t
    fi
    cd ../
fi

docker-compose up -d
