# RentalMoviesChallenge
Project for rental movies in order to handle user who rental movies

# Requirements

1. Only users with admin role are allowed to perform the following actions:
      1. Add a movie
      2. Modify a movie
      3. Remove a movie
      4. Delete a movie

2. Movies must have a title, description, at least one image, stock, rental price, sale price and availability.
3. Availability is a field of movies, which may only be modified by an admin role.
4. Save a log of the title, rental price and sale price updates for a movie.
5. Users can rent and buy a movie. For renting functionality you must keep track when the user have to return the movie and apply a monetary penalty if there is a delay.
6. Keep a log of all rentals and purchases (who bought, how many, when).
7. Users can like movies.
8. As an admin I’m able to see all movies and filtering by availability/unavailability.
9. As an user I’m able to see only the available movies for renting or buying.
10. The list must be sortable by title (default), and by popularity (likes).
11. The list must have pagination functionality.
12. Search through the movies by name.

# Security requirements

1. Add login/logout functionality.Preferably JWT.
2. Only admins can add/modify/remove movies.
3. Only logged in users can rent and buy movies.
4. Only logged in users can like movies.
5. Everyone (authenticated or not) can get the list of movies.
6. Everyone (authenticated or not) can get the detail of a movie.
7. Publish your work using heroku andsharethe link with us.

# Extra credit

1. Recovery and forgot password functionality (send email).
2. Confirming account (send email)
3. Build a small frontend app and connecting to the API.
4. As an user with admin role I want to be able to change the role of any user.
5. Unit test, at least 80% of coverage.
6. Include a dockerfile for production deployments.

# Solution
  
  ## Architecture and Design Implemented:
    -Mediator
    -Clean Architecture
    -Dependency Injection
    -DDD
  ## Frameworks, Package and Tools:
    -.Net Core 3.1
    -EF Core
    -MediatR
    -Automapper
    -Fluent API
    -XUnit
    -FakeItEasy
    -MailKit
    -Swashbuckle
    -Identity
    -FLuentAssertions
    
 # Setup
 1. Clone the repository
 2. Database Migrations:
    If you want to add new changes in the database:
      - To use dotnet-ef for your migrations please add the following flags to your command (values assume you are executing from repository root)      
      - For example, to add a new migration from the root folder:
            - **_dotnet ef migrations add "InitMigration" --project src\RentalMovies.Infrastructure --startup-project src\RentalMovies.Presentation --output-dir Data\Migrations_**
      - or you can do the following: Move to the project src\RentalMovies.Infrastructure with PowerShell and then execute the following:
            1)  **_dotnet ef migrations add "InitMigration"  --startup-project ..\RentalMovies.Presentation\ --output-dir .\Data\Migrations_**
     - If you want to install the database:
       Move to the root folder ot the project, and execute the following command:
            **1)  dotnet ef database update --project src\RentalMovies.Infrastructure\ --startup-project src\RentalMovies.Presentation\ **
 3. Run the API
    To run the API you can move to the project RentalMovies.Presentation with PowerShell with administration privilegies and then execute the following command:
      1) **_dotnet run_**
    
    To watch the the API executed you can to the url https://localhost:5001/swagger
    _Example:_
    ![alt text](https://github.com/ever1509/RentalMoviesChallenge/images/APIDemo.jpg?raw=true)
      
      
      
    
 



