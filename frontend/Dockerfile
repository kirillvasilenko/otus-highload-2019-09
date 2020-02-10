FROM node:12.15-alpine

COPY .next app/.next
COPY package.json app/
COPY next.config.js app/
COPY node_modules app/node_modules
COPY server.js app/

WORKDIR app

ENTRYPOINT ["yarn", "start"]
