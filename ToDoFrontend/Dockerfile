# Use Node.js for building the Angular application
FROM node:20.19.1-alpine as build

WORKDIR /app

# Pass backend URLs as build arguments
ARG TASK_MS_BASE_URL
ARG USER_MS_BASE_URL

# Set environment variables in the Angular build
ENV TASK_MS_BASE_URL=$TASK_MS_BASE_URL
ENV USER_MS_BASE_URL=$USER_MS_BASE_URL

# Copy only package.json and package-lock.json first to leverage Docker caching
COPY package*.json ./

# Install dependencies
RUN npm install && npm install -g @angular/cli

# Copy the application files to the container
COPY . .

# Build the Angular application
RUN ng build --configuration=production

# Use Nginx to serve the application

FROM nginx:1.28.0-alpine

# Copy the built Angular app into the Nginx HTML directory
COPY --from=build /app/dist/to-do-frontend/browser /usr/share/nginx/html

# Copy the custom Nginx configuration
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
