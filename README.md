# Introduction 
4th semester Datamatiker project for UCN.

An implementation of the [Giv et Praj](https://www.givetpraj.dk/) concept, with the front-end being written in Vue, Ionic, and TypeScript as a PWA, and the back-end being a RESTful API written in C# using ASP.NET Core with Clean Architecture.

# Getting Started
Make sure .NET 6 is installed, alongside at least version 12 of Node.js is installed.

# Build and Run
## Front-end
Go to the root path of `src/Web/PublicApp` and install all dependencies by using the following commands
```bash
$ npm i
```
To run the application in development use
```bash
$ npm run dev
```
To build the application for production use
```bash
$ npm run build:vite # Build it
$ npm run serve # Serve the dist folder locally
```

## Back-end
Either run the WebApi project in  the solution via an IDE, or run this command
```bash
$ dotnet run WebApi
```
