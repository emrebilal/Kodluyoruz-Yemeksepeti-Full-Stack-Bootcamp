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

[InMemoryCarRepository.cs](https://github.com/emrebilal/Kodluyoruz-Yemeksepeti-FullStack-Bootcamp/blob/main/Back-End/emrebilal-be-hw2/Week2_WebAPI/Data/InMemoryCarRepository.cs)

Now let's configure the **"InMemoryDatabase"** link in the **"Startup"** class in the project root.
```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddDbContext<CarsAPIContext>(x => x.UseInMemoryDatabase("RentACar"));
    services.AddTransient<ICarRepository, InMemoryCarRepository>();
}
```
Now that we have set up the Data layer, we can switch to our Web API controls. Creating a Web API Controller;  
First of all, if there are files created by default when we first create the project, we delete them. Now we create a class called **"CarsController"** in the **"Controllers"** folder, we derive this class from **Controller**. We can define the controller as the layer that receives and transmits requests from users. We do GET, POST, PUT, DELETE operations here.

[CarsController.cs](https://github.com/emrebilal/Kodluyoruz-Yemeksepeti-FullStack-Bootcamp/blob/main/Back-End/emrebilal-be-hw2/Week2_WebAPI/Controllers/CarsController.cs)
## Model Validation
The clients make a request by sending certain parameters to the API we have made, and there is a continuous data exchange. As developers we should never rely on submitted inputs. After passing the parameters sent to the methods through some security steps **"Validation"**, we should continue or discontinue the transactions.  
By using various attributes in **System.ComponentModel.DataAnnotations** in ASP.NET, we can ensure that appropriate values are entered into models. It will not be enough to just add the validation feature. In addition, verification must be checked within the Controller.  
In this project, we control it with **ModelState.IsValid** in the method in the Controller, using the default attributes we have defined on the Car model and the attributes we have written as custom. When there is a validation error, we return the message to the client as a response. In addition, in the Controller, we check whether the incoming data is "null" and if it is added with Id (if there is same Id) data.
```c#
public class Car
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Brand cannot be blank!")]
    public string BrandName { get; set; }
    [Required(ErrorMessage = "Color cannot be blank!")]
    public string Color { get; set; }
    [Required(ErrorMessage = "Year cannot be blank!")]
    [MaxYear(ErrorMessage = "Invalid year!")]
    public int ModelYear { get; set; }
    [Required(ErrorMessage = "Price cannot be blank!")]
    [MinPrice(ErrorMessage = "Price must be equal or greater than 50!")]
    public decimal DailyPrice { get; set; }
}
```
## Mapping Extension
Database tables have multi-column structures. The tables here are defined as our model objects. We use this data returned by various queries. But we may not always need all the information returned. For example, out of 30 rotating columns, we sometimes show 4-5 columns on the user side. In this case, we do a map operation between our object we created.  
**Mapping**, in short, transfer between objects.  
We create the **"CarDTO"** class under the **"Model"** folder to show our model in the project on the user side. Since there are not many features in our model with this structure, we map same features except Id.
```c#
public class CarDTO
{
    public string BrandName { get; set; }
    public int ModelYear { get; set; }
    public string Color { get; set; }
    public decimal DailyPrice { get; set; }
}
```
Now we can start writing our own mapping extension. But first, let's talk about Extension methods.  
**Extension** methods are a structure that allows us to easily extend that type without making any changes on a type. To put it more simply, these extension methods we have defined are inherently static methods we know.  
We add a new folder named **"Mapping"** under the project root. We create the **"MappingExtension"** class in this folder.
```c#
public static class MappingExtension
{
    public static List<CarDTO> ToViewModel(this List<Car> cars)
    {
        List<CarDTO> resultItems = new List<CarDTO>();
        foreach (var item in cars)
        {
            resultItems.Add(new CarDTO
            {
                BrandName = item.BrandName,
                ModelYear = item.ModelYear,
                Color = item.Color,
                DailyPrice = item.DailyPrice
            });
        }
        return resultItems;
    }
}
```
After making the final arrangements in the project, we can move on to the testing phase.
## Test with POSTMAN

