## Content:
- Creating a Web API using EF Core In-Memory
- Using Mapping Extensions Interface and/or Abstract Validation
- Testing with PostMan
- Writing an article on the project
## Introduction
In this article, I tried to explain the concept of Web API, how you can create and use a Web API with ASP.Net Core using Visual Studio. In the scenario the API, can create, list, delete and update operations in a car rental system.

Before starting, let's talk about the concept of API and a few points.
## What is API?
API (Application Programing Interface) is a structure that allows the client to use certain services (data reading, writing, deleting, editing, etc.) on the server, and the control is in the hands of the software developer. APIs that are opened to communication with the outside world using the HTTP protocol are called with certain URL Paths. These paths are also called as "Rest API".

![1](https://user-images.githubusercontent.com/46905124/111774202-3210eb00-88c0-11eb-8216-ede4d1b05ed0.png)

## What is REST?
REST is the architecture that provides communication between Client-Server via HTTP protocol. Services developed with the REST architecture are called RESTful services. The operations to be performed in RESTful services are based on the realization of the HTTP request type.  
RESTful services work independently of the platform, as them use the HTTP protocol. All clients using the HTTP protocol can be responded to. Generally, response formats are seen as JSON, XML, CSV and HTML.

![2](https://user-images.githubusercontent.com/46905124/111776115-ad739c00-88c2-11eb-811f-0de17d29933f.png)

## HTTP Methods
RESTful services use HTTP methods GET, POST, PUT, DELETE methods in data exchange. It is used for GET data reading, POST data sending, PUT data updating, DELETE data deleting.  
- **GET** method type, it is used to retrieve recorded information on the back-end using the information given from the URL. There is not Request Body.
- **POST** method type, it is generally used to send new information, to create a record on the back-end side. It has Request Body, its use is optional.
- **PUT** method type, it is used to update a registered information. It is common to send id information of the record to be updated in the URL. If the URL does not contain id information, all required information is sent from the Body.
- **DELETE** method type, it is the type of request sent to delete a registered information. It can be sent via Request Body, but usually the process is completed by sending only the id information of the information to be deleted in the URL.
## Response Code
