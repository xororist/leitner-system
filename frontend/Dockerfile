ARG NODE_VERSION=21.7.1

FROM node:${NODE_VERSION}-alpine as base

WORKDIR /usr/src/app

COPY package.json .
COPY yarn.lock .

RUN yarn install 

COPY . .

ENV NODE_ENV development

# The application is run in dev mode to be able to use nuxt ui pro without licence
CMD ["yarn", "dev"]
