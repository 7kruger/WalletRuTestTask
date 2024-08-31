FROM node:18 as build-stage

WORKDIR /app

COPY WalletRu.Client/package*.json ./

RUN npm install

COPY WalletRu.Client/. .

RUN npm run build

FROM nginx:stable-alpine as production-stage

COPY --from=build-stage /app/dist /usr/share/nginx/html

COPY WalletRu.Client/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 8000

CMD ["nginx", "-g", "daemon off;"]