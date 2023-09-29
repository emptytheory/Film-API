# Film API
Film API is an ASP.NET Core Web API designed to store and manipulate movie characters, their respective movies, and the franchises these movies belong to. This solution uses Entity Framework with a Code First approach for database interactions.

### Setup
Prerequisites:
* Visual Studio 2022 with .NET 7.
* SQL Server Management Studio.
#### Installation:
* Set up your own instance of SQL Server
* Clone this repository.
* Open the solution in Visual Studio 2022.
* Using NuGet, install the following packages:
  * AutoMapper.Extensions.Microsoft.DependencyInjection
  * Microsoft.EntityFrameworkCore.Design
  * Microsoft.EntityFrameworkCore.SqlServer
  * Microsoft.EntityFrameworkCore.Tools
  * Swashbuckle.AspNetCore
* Edit appsettings.json file so it points to your own SQL Server instance.
* In the Package Manager Console, run `Add-Migration InitialDb` followed by `Update-Database` to initialize the db. The db should contain 3 characters, 3 movies and 2 franchises at this point.
* Build and run the solution. This will launch a Swagger interface in the browser through which the endpoints can be tested.

## Features
* The API supports basic CRUD operations for Characters, Movies, and Franchises.
In addition, there are endpoints for the following:
* Update characters in a movie.
* Update movies in a franchise.
* Fetch all characters in a movie.
* Fetch all characters in a franchise.

## Documentation
The API is documented using Swagger/Open API, and all routes and details can be found in the Swagger documentation once the application is running.

## Contributers
* Michael Mørk Amundsen
