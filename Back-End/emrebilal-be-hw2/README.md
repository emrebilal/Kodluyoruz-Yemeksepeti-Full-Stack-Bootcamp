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
When the HTTP requests are completed, a response comes to us with the response code. These response codes have standard equivalents. By checking the response codes, we can get an idea of whether the request was successful or not. For detailed examination, there may be situations where we need to check the response body.  
The most commonly used response codes are:
- 200: OK
- 201: Created
- 204: Not Content
- 400: Bad Request
- 401: Unauthorized
- 404: Not Found
- 409: Conflict
- 500: Internal Server Error
- 503: Service Unavailable
## Creating the Project
In the Visual Studio environment, we create an ASP.Net Core Web Application project at the stage of creating a new project. After entering our project information, we continue by selecting "API" from the screen that appears.  
Now we start by creating our model, for this we create a folder named **"Models"** in the root directory of the project and create a class named **"Car"** in it.
```c#
public class Car
{
    public int Id { get; set; }
    public string BrandName { get; set; }
    public string Color { get; set; }
    public int ModelYear { get; set; }
    public decimal DailyPrice { get; set; }
}
```
Then we create another folder named **"Data"**, we will use **"EF Core: In-Memory Database"** to save our data. The purpose of this is to do our operations on memory without creating a real database to test the API.  
We need to adjust the Entity Framework Core, for this we add the **"Microsoft.EntityFrameworkCore.InMemory"** package from the **"NuGet Package Manager"** section.  
Now, we open another folder called **"Context"** into the **"Data"** folder, create a class called **"CarsAPIContext"** and derive it from **"DbContext"**, we will ensure the communication between our application and our data provider.
```c#
public class CarsAPIContext : DbContext
{
    public CarsAPIContext(DbContextOptions options) : base(options)
    {

    }
    
    public DbSet<Car> Cars { get; set; }
}
```
We create an **"Abstract"** folder under the **"Data"** folder, create an interface class called **"ICarRepository"** and add our methods.
```c#
public interface ICarRepository
{
    IEnumerable<Car> GetAll();
    IEnumerable<CarDTO> GetUI();
    Car GetById(int id);
    Car Add(Car car);
    void Update(Car car);
    void Delete(Car car);
}
```
Under our **"Data"** folder, we create another class named **"InMemoryCarRepository"** to communicate with our in-memory database. We implement it from the "ICarRepository" class. We are now ready to write our methods.  
[InMemoryCarRepository.cs](Back-End/emrebilal-be-hw2/Week2_WebAPI/Data/InMemoryCarRepository.cs)


