### ELearningAPI Technical Test

## Technology stack:  
   - NET 8
## Architecture 
   I choose a 4 layer architecture with 4 projects in the same solution
   - ELearningAPI.Models :
     - this layer consists on the creation of all domain entities , enums , dtos and ViewModels it's the domain layer
   - ElearningAPI.Infrastructure:
     - this layer consists of repositories , data context , anda data initialization it's the connection of the external sources of data
   - ElearningAPI.Application :
     - this layers manages all of the services required for the Presentation layer , business logic and other responsabilities like authentication and authorization
   - ElearningAPI.Presentation :
     - this layer represents the interaction of the API with clients , has the controllers that exposes the endpoints and program.cs to initialize and starts the API

## Additional Details , Tools and Libraries used in this Architecture
   - Data management in this arquitecture is thanks to the repository pattern and an Inmemory Database provided by EntityFrameworkCore
   - I have Installed Automapper but at the end I changed my mind and use the old plain mapping at hand, this for configuration issues and time, but i Recommend to use Automapper whenever you can
   - Authorization and Authentication are provided with JSON webtokens with the native functionality of NET 8
   - There is a login endpoint with dummy users to log in and create tokens to get access to some endpoints in controllers
   - Every layer of this project have a ServiceExtension class that allow to manage all of the code of configuration of services and dependency injection in a better way that using only program class alone

## Ways to use this project 
   - download visual studio code 2022 and open up this project with it
   - use postman or another client to work with services
   - first of all you need to get a token in the Authorization endpoint for this here are the dummy users
        - User: homer@fakemail.com , Password: homerpass , Type: Professor
        - User: bart@fakemail.com , Password: bartpass , Type: Student
