# Bike Stores Management

![](https://github.com/qasimshk/CleverTalker/blob/master/BikeStoreDiagram.png)

A bike store management system is developed using the DotNet five framework, which is based on a microservice design pattern. In this project, I have used three microservices ( order, inventory, notification ) which is controlled by another microservice called orchestrator. This microservice contains all the workflows. 

Workflows are based on sagas pattern where every transaction action is predefined and services are instructed by the orchestrator to act accordingly. In case of any failure, the orchestrator will roll back the process or break it with the proper error message stored in log files.

## Build Status 

| Service | Status |
| ------------- | ------------- |
| Bike Store | [![Build status](https://dev.azure.com/CematixSolutions/CT%20Microservices/_apis/build/status/gateway-microservice-ci)](https://dev.azure.com/CematixSolutions/CT%20Microservices/_build/latest?definitionId=5) |


### Gateway Service

To develop a gateway for all my the services to be accessible from one URL, I used the Ocelot API gateway NuGet package which is an open-source project 

### CQRS Pattern & Clean Architecture

To develop an application on clean architecture, I have used the MediatR NuGet package which will separate, all the commands and queries handlers in the service.

### Saga Pattern

For my windows services and saga pattern, I have used the Masstransit NuGet package. This package plays an important role in developing and workflow building of every microservice.

### Authentication and Authorization

All services can only be accessible using a token, which is generated using the Identity Server 4 NuGet package. ID4 is one of the best packages to implement a single sign-in concept. 


### NuGet Packages

- [MassTransit](https://masstransit-project.com/getting-started/)
- [MediatR](https://github.com/jbogard/MediatR)
- [Ocelot](https://ocelot.readthedocs.io/en/latest/index.html)
- [IdentityServer4](https://identityserver4.readthedocs.io/en/latest/)
