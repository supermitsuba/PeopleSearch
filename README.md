The People Search Application ![](https://travis-ci.org/supermitsuba/PeopleSearch.svg?branch=master)
====================================================================================================

Business Requirements
---------------------

- The application accepts search input in a text box and then displays in a pleasing style a list of people where any part of their first or last name matches what was typed in the search box (displaying at least name, address, age, interests, and a picture). 
- Solution should either seed data or provide a way to enter new users or both
- Simulate search being slow and have the UI gracefully handle the delay

Technical Requirements
----------------------

- An ASP.NET MVC Application 
- Use Ajax to respond to search request (no full page refresh) using JSON for both the request and the response
- Use Entity Framework Code First to talk to the database
- Unit Tests for appropriate parts of the application

Quick Links
-----------

Please visit [localhost](http://localhost:8000/)

API documentation is located here: [Swagger Documentation](https://github.com/supermitsuba/PeopleSearch/tree/master/documentation)

CI Builds use [Travis CI](https://travis-ci.com/).  They are located here: [PeopleSearch CI](https://travis-ci.org/supermitsuba/PeopleSearch)

Deployment Scripts are located here: [Docker deployment README](https://github.com/supermitsuba/PeopleSearch/tree/master/deployment/docker)

Entity Setup : [Setup Guide](https://docs.microsoft.com/en-us/ef/core/get-started/netcore/new-db-sqlite)

- Initialize a database: ```dotnet ef migrations add MyFirstMigration```
- Update a database: ```dotnet ef database update```

Releases
--------

You might notice that with each feature there is a release created.  This should show how the project evolved with each new feature being added.

Application Checklist
---------------------

- [ ] Code
  - [ ] Logger - Log4Net
  - [ ] Caching and Redis ?
  - [ ] Moq
  - [ ] DI/ IOC -> Unity?
  - [ ] Add production/dev environment
- [ ] Usage Documentation
- [ ] Tradeoffs
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
  - [ ] CSS Preprocessor
  - [ ] JS minify and Bundle
  - [ ] Unit Tests
  - [X] Twitter Bootstrap
- [X] SQLite
  - [ ] SQL Scripts
  - [X] Entity Framework
- [X] Add Unit Test Project
  - [ ] Write Unit Tests
- [X] API Documentation
  - This is at [Swagger documentation](https://github.com/supermitsuba/PeopleSearch/tree/master/documentation)
- [X] CI
  - Travis CI is working for .net CORE
  - build.sh needs ```chmod a+x build.sh``` to work properly
- [X] Static Code Analysis
  - Enabled [style cop analyzer](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/Configuration.md)
- [X] Deployments
  - [X] Docker Scripts ?
  - [ ] Scripts to deploy to Azure?
- [X] Name Generator?
  - use this API https://uinames.com/api/?amount=25&region=united+states
- [X] Github Releases and tags
- [X] ~~Performance Tests ?~~
  - Not doing performance tests