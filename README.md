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
   - Some precharged data are created on the DbInitializer.cs on ElearningAPI.Infraestructure/Data give it a look to get the ids and test the endpoints 

## How to use this project 
   - download visual studio code 2022 and open up this project with it
   - use postman or another client to work with services
   - I have added a exec.zip folder on this repository you can download it on your machine and open it
   - you need to get net 8 runtime first
      - https://download.visualstudio.microsoft.com/download/pr/751d3fcd-72db-4da2-b8d0-709c19442225/33cc492bde704bfd6d70a2b9109005a0/dotnet-hosting-8.0.6-win.exe
   
   - when the runtime is installed you need to run dotnet --version to verify 
   ![image](https://github.com/JSalCarr1989/ELearningAPI/assets/33041463/87df38c5-7c32-4699-a4d4-5cc09c426a54)

   - after that you need to open cmd on the exec folder and execute this command dotnet ELearningAPI.Presentation.dll --urls https://localhost:7001
   
   - the api should be running like this
   ![image](https://github.com/JSalCarr1989/ELearningAPI/assets/33041463/3ed5b031-62f0-4eee-bfc9-01670cff5a2d)

   - finally use postman to test the API
   ![image](https://github.com/JSalCarr1989/ELearningAPI/assets/33041463/456b1a8c-ced6-4404-94d3-e5aa224ca7c0)


## Testing API
   - First of all you need to get a token in the https://localhost:7001/api/Auth/login endpoint for this here are the dummy users
        - User: homer@fakemail.com , Password: homerpass , Type: Professor
        - User: bart@fakemail.com , Password: bartpass , Type: Student
   - take the token from the login endpoint response and use it as a Bearer Adding in Authorization token in consecutive requests
   - Create courses With Post Method on https://localhost:7001/api/Courses endpoint look for location header in the response and take the id you'll need it to create lessons
   - Create Lessons With Post Method https://localhost:7001/api/Lessons endpoint look for location header in the response and take the id you'll need it to create questions
   - Create Questions With Post Method https://localhost:7001/api/Questions endpoint
   - There is an AnswersController to send all answers of n questions in a go
   - There is an Available/id endpoint on Courses to check the courses status and courses available for a student based on the progress of the student answers
   - There is an Available/id endpoint on Lessons to check the available lessons based on a course id and the process of the student answers
 ## TODO
   - change all plain mapping to automapper.
   - implement a strong based secureapikey on settings to generate jsonwebtokens
   - implement serilog to log data in the sources you like
   - add a testing project with xUnit test framework , add related unit tests and integration tests
   - add personalized types of exceptions for every validation needed
   - add a global middleware to manage every error globally and check for every type of exception
   - add an Enroll endpoint on Courses
