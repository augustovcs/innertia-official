FROM node:23.11.0-alpine

RUN mkdir -p /src && chown -R node:node /src

WORKDIR /src

COPY package.json .

USER node

COPY --chown=node:node . .

CMD [ "npm", "run", "dev" ]

EXPOSE 5173