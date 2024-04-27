# Use the official Node.js image as the base
FROM node:latest AS build
WORKDIR /app

# Copy package.json and install dependencies
COPY package.json ./
RUN npm install

# Copy the remaining source code and build the application
COPY . .
RUN npm run build --prod

# Expose the port for Angular server
EXPOSE 4200

# Command to start the Angular server
CMD ["npm", "start"]