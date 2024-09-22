# InitialProject

This project is built with .NET Core 8. It includes a dashboard, API, and identity management. This readme provides initial project details and setup instructions.

## Features

- **.NET Core 8**: The project is developed using the latest .NET Core 8 framework.
- **Dashboard**: A user-friendly dashboard to manage the application.
- **API**: Ready-to-use APIs for various functionalities.
- **Identity**: Integrated identity management for user authentication and authorization.

## Prerequisites

Before you begin, ensure you have met the following requirements:
- [.NET Core SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or any other code editor
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or any other database system

## Getting Started

To set up the project locally, follow these steps:

1. **Clone the repository:**
    ```bash
    https://github.com/AhmedShahine1/InitialProject.git
    cd InitialProject
    ```

2. **Install dependencies:**
    Ensure you have all the required libraries installed. Most dependencies will be installed via NuGet. Here are some of the important ones:
    - `Microsoft.EntityFrameworkCore.SqlServer`
    - `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
    - `Swashbuckle.AspNetCore` for Swagger
    - `System.Text.Json`
    - `Microsoft.Extensions.Configuration`

    You can install these packages using the NuGet Package Manager or the CLI:
    ```bash
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
    dotnet add package Swashbuckle.AspNetCore
    dotnet add package System.Text.Json
    dotnet add package Microsoft.Extensions.Configuration
    ```

3. **Configure the application:**
    - Update `appsettings.json` with your database connection string and other necessary configurations.

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InitialProject;Trusted_Connection=True;MultipleActiveResultSets=true"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*"
    }
    ```

4. **Setup the database:**
    Apply migrations and update the database.

    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

5. **Run the application:**
    Use the following command to run the application.

    ```bash
    dotnet run
    ```

    The application will start on `https://localhost:5001` or `http://localhost:5000` by default.

## Project Structure

- **Controllers**: Contains API controllers that handle HTTP requests and responses.
- **Models**: Contains the data models used throughout the application.
- **Views**: Contains Razor views for the dashboard.
- **wwwroot**: Contains static files like JavaScript, CSS, and images.
- **Configurations**: Contains configuration files like `appsettings.json`.

## API Documentation

The project uses Swagger for API documentation. Once the application is running, you can access the API documentation at `https://localhost:5001/swagger`.

## Contributing

To contribute to this project, follow these steps:

1. Fork this repository.
2. Create a branch: `git checkout -b feature-branch-name`.
3. Make your changes and commit them: `git commit -m 'Add some feature'`.
4. Push to the original branch: `git push origin main`.
5. Create the pull request.

## License

This project is licensed under the MIT License - see the LICENSE.md file for details.

## Acknowledgements

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [Swagger Documentation](https://swagger.io/docs/)

## Contact

If you want to contact me, you can reach me at hmad32003@gmail.com.

