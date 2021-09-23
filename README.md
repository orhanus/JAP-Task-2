# MovieApp-JAP-Task-2

This repository contains both frontend and backend for a movie rating app called "Rotten potatoes". Angular was used for the frontend, and .NET 5 for backend.

## Setup instructions
* Clone this repository to your local machine.
* Run ```npm install``` in "client" directory to install all the needed packages.
* Backend uses SqlServer database. Make sure you have it installed. Database creation and seeding of data is done automatically when the application is being run.
* Inside the "API' directory run ```dotnet watch run``` or ```dotnet run``` in the terminal to start web api.
* To start the client run ```ng serve``` inside the "client" directory


## Functionalities API
* Route to rate movies or shows from 1 to 10
* Route to get movies or shows with search parameters and pagination, where the maximum number of items per page is 50

## Tests API
* Tested adding rating functionality
* Tested average string parser
* Tested average rating calculation
* Tested buy ticket functionality
