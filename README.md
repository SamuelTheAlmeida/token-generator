# TokenGenerator API

Microservice for cashless registration

# Run with docker

 - On src folder:
 
`$ docker build . -t token-generator -f TokenGenerator.Api/Dockerfile`

`$ docker run -d -e "ASPNETCORE_ENVIRONMENT=Development" -e "ASPNETCORE_URLS=http://+:80" -p 8080:80 --name TokenGenerator.Api token-generator`

API will be running on localhost:8080

## Features
- .NET 5 WebApi
- Swagger docs
- Unit Tests with NUnit, Moq and Bogus
- Logging and Exception Handling Middlewares
- In-Memory database
