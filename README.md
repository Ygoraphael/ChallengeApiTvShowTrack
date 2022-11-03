# Challenge TEKEVER TvShow Tracker API

## Documentation of the API

There is a file on this project named "openApi.yaml" that is the documentation of the api on Swagger utilized
to implement the API. Link bellow:
- [https://github.com/Ygoraphael/ChallengeApiTvShowTrack/blob/main/openApi.yaml](https://github.com/Ygoraphael/ChallengeApiTvShowTrack/blob/main/openApi.yaml).

## How to run.

Necessary instal docker on machine to run the app

Before starting the project it will be necessary to execute the file "JustDB.ps1". This file creates a container docker image with sql server image and after will
run the command to update migrations of the entity framework to create the database inside of the container running on the door 1433.

Now you can run the application on visual studio.

At the beginning there is no data on the database so the background worker will run and seed it.
To see informations about the backgroundworker HangFire you access by your browser on the path that the application is
running like this:
- [https://localhost:7291/hangfire/jobs/enqueued](https://localhost:7291/hangfire/jobs/enqueued).

This path will show you the dashboard of hangfire which will show you that there is a task scheduled Weekly.

Now when you run the project locally the API, access the link below:
 - [http://localhost:7291/swagger/index.html](http://localhost:7291/swagger/index.html)


## Describe the Implementation of the API.

To start the project I created the documentation on swagger as a kind of contract with the client and also to help me
visualize what would be necessary to implement.

On the web service all other queries on the database are made by entity framework, all endpoints that return some collection
will return a default structure where they have some information of the pagination and the data.

All the endpoints that have a collection return have the option on query to indicate what page and size and sort that consult
will return. Sorts follow the default (column).(asc|desc) and can have all the columns separated by "," example id.asc,name.desc.

This project has integration tests implemented on all endpoints checking possible results that it will return.
To implement the tests I used the framework nUnit.

Since I could choose any source to import data to the local database I chose The Movie Database API where I used the hangfire as a
background worker because this is a free library for .net that manages all the necessities for this project.
It will load data from the source weekly and if any error happens on this process hang fire take care of retry the process.

I created a class called ApiServiceTMDB where it will call the api and save the data on the database, for this service I used
dapper to persist the data bause it increased the performance on this task compared to entity framework.
