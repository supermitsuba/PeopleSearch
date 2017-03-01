#The People Search Application ![](https://travis-ci.org/supermitsuba/PeopleSearch.svg?branch=master)

## Business Requirements

- The application accepts search input in a text box and then displays in a pleasing style a list of people where any part of their first or last name matches what was typed in the search box (displaying at least name, address, age, interests, and a picture). 
- Solution should either seed data or provide a way to enter new users or both
- Simulate search being slow and have the UI gracefully handle the delay

### Technical Requirements

- An ASP.NET MVC Application 
- Use Ajax to respond to search request (no full page refresh) using JSON for both the request and the response
- Use Entity Framework Code First to talk to the database
- Unit Tests for appropriate parts of the application

### Prerequisite

- .Net Core 1.0.3 SDK
- Visual Studio .net or Visual Studio Code
- Linux/Windows/Mac

### Things I missed

Note that these are things I wanted to do that were out of scope, but might be valid to add to the project.

- Address lookup for validating
- Shrink pictures for search to make the results faster
- Color and styling
- Interests picker (As you type an interest, it adds it in a list format)

### Quick Links

Please visit [localhost](http://localhost:8000/people)

API documentation is located here: [Swagger Documentation](https://github.com/supermitsuba/PeopleSearch/tree/master/documentation)

CI Builds use [Travis CI](https://travis-ci.com/).  They are located here: [PeopleSearch CI](https://travis-ci.org/supermitsuba/PeopleSearch)

Deployment Scripts are located here: [Docker deployment README](https://github.com/supermitsuba/PeopleSearch/tree/master/deployment/docker)

[Usage](https://github.com/supermitsuba/PeopleSearch/tree/master/documentation/Usage) 

Entity Setup : [Setup Guide](https://docs.microsoft.com/en-us/ef/core/get-started/netcore/new-db-sqlite)

- Initialize a database: ```dotnet ef migrations add MyFirstMigration```
- Update a database: ```dotnet ef database update```

### Releases

You might notice that with each feature there is a release created.  This should show how the project evolved with each new feature being added.

## Tradeoffs

### ASP.net CORE

Asp.net core is a new version of .net.  The reason to use it is to do cross platform develoment.  The downside is that the framework is not equal to eariler versions.  This means that things that worked before dont work now.  The upside of using .net core is that you can use open source tools.  Travis CI and Docker are not accessible easily using .net core.

### Images

Right now the images are show full resolution, then shunk down using css.  We should instead taking the image and reprocessing it.  This would make a great customer experience by reducing the number of bytes that are sent.

### SQLite vs SQL Server

I used SQLite because SQL Server local files could not be used on linux/mac.  A mac or linux client can connect to a SQL Server though, so it isnt all bad.  SQLite is universal and so I could use this no problem.

### xunit vs MSTest

MSTest is great to use because it is built into VS.net.  The problem for cross platforms is that MSTest does not work on Mac and Linux without mono.  XUnit works on all platforms and has test runners that integrate into VS.net.

### Docker Linux vs Docker windows

Docker linux is more accessable than Docker windows.  This is because Docker windows only works on windows 10 +.  This makes it difficult to use on Windows 7.

### Database vs In Memory

I used a combination of in memory and database calls.  I figured that each Person record is about 1000 Bytes (just look at the length and add some fluff).  Because of this, we could say that we could store around 100,000 records in memory and it would only consume 100 MB.  Potentially we could store up to a million results to give 1 GB, and possibly 10's of millions of uses.  This makes access fast, especially using a Trie.  If the number of records exceed 100,000, then we fail back to Database queries.  We do this by using a Chain of Responsibility design pattern.

### Distrubuted vs Single machine

We are using a single machine in our estimations.  Caching in memory would not work if there are multiple machines.  This is because if one computer gets an update, we would need to update all the other machines.  Instead, we could use a distrubted cache server, like redis, to cache data.

### SPA vs post-back 

SPA web sites are great for responsive web UI's.  They also reduce the amount the round trip data being sent, but there are downsides.  One is that if javascript is disabled, then a SPA page does not work.  Also, if you are wanting SEO to pick up on the site, then SPA would not render.  If you wanted to use a SPA, it would have to be an internal application.  If you needed SEO, then you might want to do partial SPA/post-back data.

### Using Front-end task manager like Grunt/Gulp/etc

Say if you wanted to build SASS or LESS CSS, JS minify or build, compile from typescript to javascript, then you want to use a task system like Grunt or Gulp.  In ASP.net core you can build the process into the workflow, but that means install Node and NPM.  One of the requests is to start the project immediately, so I was hesitant to add this into the project.

## Application Checklist

- [X] Code
  - [ ] ~~~Shrink Images~~~
    - Not enough time
  - [X] Logger - Log4Net
  - [X] Caching and Redis ?
  - [X] Complete mass import call
  - [X] ~~Moq~~
    - Using Fakes
  - [X] DI/ IOC -> Unity?
  - [X] Add production/dev environment
  - [X] Add logging
  - [X] Return Error Codes
- [X] Usage Documentation
- [X] Tradeoffs
  - SQLite vs SQL Server
  - ASP.net CORE vs .net Framework 4.6
  - windows vs cross platform
  - xunit vs MSTest
  - Docker linux vs Docker windows
  - Database vs In Memory
  - Distrubuted vs single machine
  - SPA vs post-back 
  - Using Front-end task manager like Grunt/Gulp/etc
- [X] Front-End Build Process ?
  - [X] Angular.js or JQuery?
  - [X] CSS
  - [ ] ~~~JS minify and Bundle~~~
    - Didnt find out how to integrate
  - [ ] ~~~Unit Tests~~~
    - Didnt get to javascript tests
  - [X] Twitter Bootstrap
- [X] SQLite
  - [X] ~~~SQL Scripts~~~
    - There is a database file
  - [X] Entity Framework
- [X] Add Unit Test Project
  - [ ] Write Unit Tests
- [X] API Documentation
  - This is at [Swagger documentation](https://github.com/supermitsuba/PeopleSearch/tree/master/documentation)
  - There is also a postman script to help with calling the APIs.
- [X] CI
  - Travis CI is working for .net CORE
  - build.sh needs ```chmod a+x build.sh``` to work properly
- [X] Static Code Analysis
  - Enabled [style cop analyzer](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/Configuration.md)
- [X] Deployments
  - [X] Docker Scripts ?
  - [ ] ~~~Scripts to deploy to Azure?~~~
    - not enough time to look into this
- [X] Name Generator?
  - use this API https://uinames.com/api/?amount=25&region=united+states
- [X] Github Releases and tags
- [ ] ~~Performance Tests ?~~
  - Not doing performance tests
