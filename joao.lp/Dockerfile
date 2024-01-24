# Use uma imagem de Node.js
FROM node:20.10.0

# Defina o diretório de trabalho dentro do contêiner
WORKDIR /usr/src/app

# Copie o package.json e o package-lock.json para o diretório de trabalho
COPY joao.lp/package*.json ./

# Instale as dependências
RUN npm install

# Copie todos os arquivos do projeto para o diretório de trabalho
COPY joao.lp .

# Compile o projeto Angular
RUN npm run build -- --configuration=production

# Exponha a porta em que o aplicativo Angular será executado
EXPOSE 4200

# Comando para iniciar o aplicativo Angular
CMD ["npm", "start"]
