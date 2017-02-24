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

SQL Scripts are located here: 

CI Builds use [Travis CI](https://travis-ci.com/).  They are located here: [PeopleSearch CI](https://travis-ci.org/supermitsuba/PeopleSearch)

Deployment Scripts are located here: [Docker deployment README](https://github.com/supermitsuba/PeopleSearch/tree/master/deployment/docker)

Application Checklist
---------------------

- [X] API Documentation
  - This is at [Swagger documentation](https://github.com/supermitsuba/PeopleSearch/tree/master/documentation)
- [X] CI
  - Travis CI is working for .net CORE
  - build.sh needs ```chmod a+x build.sh``` to work properly
- [ ] Code
  - [ ] Logger - Log4Net
  - [ ] Caching and Redis ?
  - [ ] Moq
  - [ ] DI/ IOC -> Unity?
- [ ] SQL Server Local DB
  - [ ] SQL Scripts
  - [ ] Entity Framework
- [X] Add Unit Test Project
  - [ ] Write Unit Tests
- [ ] Front-End Build Process ?
  - [ ] Angular.js
  - [ ] CSS Preprocessor
  - [ ] JS minify and Bundle
  - [ ] Twitter Bootstrap
- [X] Static Code Analysis
  - Enabled [style cop analyzer](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/Configuration.md)
- [X] Deployments
  - [X] Docker Scripts ?
  - [ ] Scripts to deploy to Azure?
- [X] Name Generator?
  - use this API https://uinames.com/api/?amount=25&region=united+states
- [ ] Performance Tests ?
- [ ] Usage Documentation
- [ ] Tradeoffs
