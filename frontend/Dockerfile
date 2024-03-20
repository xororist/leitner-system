ARG NODE_VERSION=21.7.1

FROM node:${NODE_VERSION}-alpine as base

WORKDIR /usr/src/app

COPY package.json .
COPY yarn.lock .

RUN yarn install --frozen-lockfile

COPY . .

ENV NODE_ENV development

CMD ["yarn", "dev"]
