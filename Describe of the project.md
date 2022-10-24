To start the project I created the documentation on swagger as a kind of contract with the client and also to help me visualize what would be necessary to implement.
On the web service all other queries on the database are made by entity framework, all endpoints that return some collection will return a default structure where they have
the data as total of results, current page and page size. All the endpoints that have a collection return have the option on query to indicate what page and size
and sort that consult will return. Sorts follow the default (column).(asc|desc) and can have all the columns separated by ",". This project has tests implemented
on all endpoints checking some results that it will return. For this I used the framework nUnit. This project its a challenge where i could load data from any source
I choose The Movie Database API where I used the hangfire as a background worker because this is a free library for .net that manager all the necessities for this
project it will load data from the source once per day, if any error happen on this process hang fire take care restart the process. I created a class called ApiServiceTMDB
where it will call the api and save the data on the database, for this service I used dapper to persist the data bause it increased the performance on this task compared to entity
framework.

