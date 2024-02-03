# Use uma imagem de Node.js
FROM node:lastest as angular

# Defina o diretório de trabalho dentro do contêiner
WORKDIR /usr/src/app

# Copie o package.json para o diretório de trabalho
COPY joao.lp/package.json .

# Instale as dependências
RUN npm install --silent

# Copie todos os arquivos do projeto (exceto node_modules) para o diretório de trabalho
COPY joao.lp .

# Compile o projeto Angular
RUN npm run build

# Exponha a porta em que o aplicativo Angular será executado
EXPOSE 4200

# Comando para iniciar o aplicativo Angular
CMD ["npm", "start", "--host", "0.0.0.0"]
