# MovieApp-JAP-Task-1

This repository contains both frontend and backend for a movie rating app called "Rotten potatoes". Angular was used for the frontend, and .NET 5 for backend.

## Setup instructions
* Clone this repository to your local machine.
* Run ```npm install``` in "client" directory to install all the needed packages.
* Backend uses Sqlite database, and it can be found on this repository with seeded data, so no setup for the database is needed.
* Inside the "API' directory run ```dotnet watch run``` or ```dotnet run``` in the terminal to start web api.
* To start the client run ```ng serve``` inside the "client" directory


## Functionalities Client
* List of Top 10 Movies sorted by rating
* Top 10 Movies and TV Shows tabs
* Search any Movie/TV Show by title, description
* Search includes special phrases such as: "5 stars", "at least 3 stars", "after 2015", "older than 3 years"
* Rate any Movie/TV Show with 1-5 stars
* Load 10 more Movies/TV Shows

## Functionalities API
* Route to rate movies or shows from 1 to 10
* Route to get movies or shows with search parameters and pagination where maximum nubmer of items per page is 50
