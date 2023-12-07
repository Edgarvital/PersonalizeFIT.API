#!/bin/bash

# Diretório onde os arquivos docker-compose.yml estão localizados
DOCKER_COMPOSE_DIR="$(dirname "$(readlink -f "$0")")"

# Caminho para o primeiro arquivo docker-compose.yml
COMPOSE_FILE1="${DOCKER_COMPOSE_DIR}/kong-docker-compose.yaml"

# Caminho para o segundo arquivo docker-compose.yml
COMPOSE_FILE2="${DOCKER_COMPOSE_DIR}/personalize-fit-docker-compose.yaml"

start_docker_kong() {
  docker-compose -f $COMPOSE_FILE1 up -d
}

# Função para iniciar o segundo contêiner
start_docker_personalize_fit() {
  docker-compose -f $COMPOSE_FILE2 up -d
}
# Iniciar os contêineres
start_docker_kong
start_docker_personalize_fit