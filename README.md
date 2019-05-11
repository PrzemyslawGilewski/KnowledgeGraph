# KnowledgeGraph

This application was developed to help you better organise and search the knowledge you are interested to keep. 

You can fastly jump from Concepts and Contents to Source, from Source to Authors and so on.

## Technology

The application was developed using **ASP.NET Core 3** framework with the use of the following libraries: Entity Framework Core (code first approach), AutoMapper and MediatR. 

The structure of the project follows Feature Folder architecture and CQRS pattern for data access and modification. 

Because this is mainly CRUD application so far, it was divided into 3 .NET projects (layers): KnowledgeGraph.Data (Class Library), KnowledgeGraph.Application (Class Library), KnowledgeGraph.Web (Web Application).

The frondend was developed using Bootstrap 4, SCSS preprocessor and jQuery.
