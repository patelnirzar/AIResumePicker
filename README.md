# AI Resume Picker

AI Resume Picker is a .NET application designed to streamline the process of selecting resumes for job applications using AI and machine learning techniques. This project leverages various libraries and frameworks to provide a robust and efficient solution for resume parsing and selection.

## Features

- **Resume Parsing**: Extracts relevant information from resumes.
- **Job Matching**: Matches resumes with job descriptions.
- **API Integration**: Provides RESTful APIs for interacting with the system.
- **Database Integration**: Uses Entity Framework Core for database operations.
- **Swagger Documentation**: Integrated Swagger for API documentation.

## Prerequisites

Before you begin, ensure you have met the following requirements:

- **.NET 9.0 SDK**: Make sure you have the .NET 9.0 SDK installed.
- **Entity Framework Core**: Used for database operations.
- **AutoMapper**: For object-object mapping.
- **Azure.AI.FormRecognizer**: For AI-based form recognition.
- **Swashbuckle.AspNetCore**: For Swagger API documentation.
- **Pomelo.EntityFrameworkCore.MySql**: For MySQL database integration.

## Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/AIResumePicker.git
    ```
2. Navigate to the project directory:
    ```sh
    cd AIResumePicker
    ```
3. Restore the dependencies:
    ```sh
    dotnet restore
    ```
4. Build the project:
    ```sh
    dotnet build
    ```

## Usage

1. Update the [appsettings.json](http://_vscodecontentref_/1) and [appsettings.Development.json](http://_vscodecontentref_/2) files with your database connection string and other configurations.
2. Run the application:
    ```sh
    dotnet run
    ```
3. Access the Swagger UI for API documentation at `http://localhost:5068/swagger/index.html`.

## Project Structure

- **Controllers**: Contains API controllers.
- **Data**: Contains the `AppDbContext` for Entity Framework Core.
- **DTOs**: Data Transfer Objects used in the application.
- **Migrations**: Database migrations.
- **Models**: Contains the data models.
- **Service**: Contains business logic and services.

## Packages

The project uses the following NuGet packages:

- `AutoMapper` (12.0.1)
- `AutoMapper.Extensions.Microsoft.DependencyInjection` (12.0.1)
- `Azure.AI.FormRecognizer` (4.1.0)
- `Microsoft.AspNetCore.OpenApi` (9.0.0)
- `Microsoft.EntityFrameworkCore` (8.0.2)
- `Microsoft.EntityFrameworkCore.Design` (8.0.2)
- `Microsoft.EntityFrameworkCore.Tools` (8.0.2)
- `Pomelo.EntityFrameworkCore.MySql` (8.0.2)
- `Swashbuckle.AspNetCore` (7.2.0)
- `System.Net.Http.Json` (9.0.1)

## Contributing

Contributions are always welcome! Please read the contribution guidelines first.

## License

This project is licensed under the MIT License. See the LICENSE file for details.