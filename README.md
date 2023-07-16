Author: Patrick <patrick.budiman@gmail.com>

# 1. Description
The Movie API is a web application built using .NET Core 7 that provides endpoints to manage a collection of movies. 
It allows users to retrieve a list of movies, get a single movie by its ID, add new movies, update existing movies, and delete movies. 
The API uses PostgreSQL as the database to store movie information.

# 2. Installation

## 2.1. Prerequisites
Before running the API, you need to ensure the following prerequisites are met:
- .NET Core 7 SDK is installed on your machine.
- PostgreSQL database server is installed and configured.

## 2.2. PostgreSQL Setup
1. Download PostgreSQL Installer:
Go to the official PostgreSQL website (https://www.postgresql.org/download/windows/) and download the latest version of the PostgreSQL installer for Windows.
2. Run the Installer:
Double-click the downloaded installer file (e.g., postgresql-x.x-x-windows-x64.exe) to start the installation process.
3. Choose Components:
During the installation, you can choose the components you want to install. The default selection should be sufficient for most users.
4. Data Directory and Port Configuration:
Select the data directory (the location where PostgreSQL will store its data files) and the port number for the PostgreSQL server. The default port is 5432, but you can choose another if needed.
5. Set Password for PostgreSQL Superuser:
During the installation, you will be prompted to set a password for the PostgreSQL superuser (postgres by default). Remember this password as you will need it later for administrative tasks.
6. Complete the Installation:
Continue through the installation wizard and let it complete the installation process.
7. Verify Installation:
Once the installation is complete, you can verify it by opening the pgAdmin tool, which comes bundled with PostgreSQL. It is a graphical management tool for PostgreSQL databases.

## 2.3. Installation Steps
1. Clone the repository to your local machine.				
2. Open the solution in Visual Studio.
3. Open the `appsettings.json` file and update the `ConnectionStrings:DefaultConnection` value to point to your PostgreSQL database server.
4. Open the Package Manager Console and Run the database migration command to create the required tables: update-database
5. Build and Run the API:
Build the solution to ensure there are no build errors.
Start the API by running the MovieAPI project.
The API should now be running and accessible at https://localhost:<port>/ (replace <port> with the port number used by the API).

# 3. API

## 3.1. Endpoints
The API provides the following method and endpoints:
GET /movies: Retrieves a list of all movies.
GET /movies/{id}: Retrieves a single movie by its ID.
POST /movies: Adds a new movie to the collection.
PATCH /movies/{id}: Updates an existing movie by its ID.
DELETE /movies/{id}: Deletes a movie by its ID.

## 3.2. Request and Response Formats
The API follows a standard response format for all endpoints:
{
  "result": {},
  "isSuccess": true,
  "message": ""
}
result: Holds the data returned from the API (e.g., movie object or list of movies).
isSuccess: Indicates whether the request was successful (true) or encountered an error (false).
message: Contains additional information or error messages in case of failure.

The API accepts and returns data in JSON format.
1. GET /movies
Request: GET /movies
Response : 
{
  "result": [
    {
      "id": 1,
      "title": "Movie 1",
      "rating": 7.5,
      "description": "A thrilling movie",
      "image": "movie1.jpg",
      "created_at": "2023-07-16T12:00:00Z",
      "updated_at": "2023-07-16T12:00:00Z"
    },
    {
      "id": 2,
      "title": "Movie 2",
      "rating": 8.2,
      "description": "An action-packed movie",
      "image": "movie2.jpg",
      "created_at": "2023-07-16T12:00:00Z",
      "updated_at": "2023-07-16T12:00:00Z"
    }
  ],
  "isSuccess": true,
  "message": ""
}
2. GET /movies/{id}
Request: GET /movies/1
Response:
{
  "result": {
    "id": 1,
    "title": "Movie 1",
    "rating": 7.5,
    "description": "A thrilling movie",
    "image": "movie1.jpg",
    "created_at": "2023-07-16T12:00:00Z",
    "updated_at": "2023-07-16T12:00:00Z"
  },
  "isSuccess": true,
  "message": ""
}
3. POST /movies
Request: POST /movies
Content-Type: application/json
{
    "title": "New Movie",
    "rating": 8.0,
    "description": "An exciting new movie",
    "image": "new_movie.jpg"
}
Response:
{
  "result": {
    "id": 3,
    "title": "New Movie",
    "rating": 8.0,
    "description": "An exciting new movie",
    "image": "new_movie.jpg",
    "created_at": "2023-07-16T12:00:00Z",
    "updated_at": "2023-07-16T12:00:00Z"
  },
  "isSuccess": true,
  "message": "Movie was created successfully"
}
4. PATCH /movies/{id}
Request: PATCH /movies/2
Content-Type: application/json
{
  "title": "Updated Movie 2",
  "rating": 8.5
}
Response:
{
  "result": {
    "id": 2,
    "title": "Updated Movie 2",
    "rating": 8.5,
    "description": "An action-packed movie",
    "image": "movie2.jpg",
    "created_at": "2023-07-16T12:00:00Z",
    "updated_at": "2023-07-16T14:30:00Z"
  },
  "isSuccess": true,
  "message": "Movie id 2 updated successfully"
}
5. DELETE /movies/{id}
Request: DELETE /movies/2
Response:
{
  "result": {},
  "isSuccess": true,
  "message": "Movie id 2 deleted successfully"
}

## 3.3. Error Handling
In case of errors, the API will return an appropriate error message in the message field of the response. For example:
{
  "result": {},
  "isSuccess": false,
  "message": "Movie id does not exist"
}
