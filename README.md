# TvShowTrackerApi
TV SHOW TRACKER API

There is a file on this project named "openApi.yaml" this is the documentation of the api on Swagger utilized to implent the API.


To run this project this project will be necessary to create the database to make it you need to have sqlserver on your computer running and know what is the connection string for that.
With this information you will need to open the solution, find the file appsettings.json and change the Connection string that is there with yours.

After will be necessary to execute some commands on the power shell follow the steps.
On power shell go to the path of the project on you computer until the folder where there is a file called TvShowTracker.csproj after that run the code bellow

Dotnet ef database update  

It created the database on SqlServer
Now you can run the application.
At the begin there is no data on database so to start for the first time the background worker that will seed the database you have to on your browser and on the path that the application is running change to something like this https://localhost:7291/hangfire/recurring probably you only will need to change the door.
This path will show you the dashboard of hangfire which will show you that there is a task scheduled. You select that and run now for the futures executions hangfire will run at the same time everyday.

Now that you have created the database and set hangfire to seed that you can go back to the page of swagger and test the endpoints.


#Few describe the Implementation of the API.
To start the project I created the documentation on swagger as a kind of contract with the client and also to help me visualize what would be necessary to implement. On the web service all other queries on the database are made by entity framework, all endpoints that return some collection will return a default structure where they have the data as total of results, current page and page size. All the endpoints that have a collection return have the option on query to indicate what page and size and sort that consult will return. Sorts follow the default (column).(asc|desc) and can have all the columns separated by ",". This project has tests implemented on all endpoints checking some results that it will return. For this I used the framework nUnit. This project its a challenge where i could load data from any source I choose The Movie Database API where I used the hangfire as a background worker because this is a free library for .net that manager all the necessities for this project it will load data from the source once per day, if any error happen on this process hang fire take care restart the process. I created a class called ApiServiceTMDB where it will call the api and save the data on the database, for this service I used dapper to persist the data bause it increased the performance on this task compared to entity framework.
