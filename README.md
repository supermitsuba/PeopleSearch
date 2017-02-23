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

API documentation is located here: 

SQL Scripts are located here: 

CI Builds use [Travis CI](https://travis-ci.com/).  They are located here: [PeopleSearch CI](https://travis-ci.org/supermitsuba/PeopleSearch)

Deployment Scripts are located here: 

Application Checklist
---------------------

- [ ] API Documentation
- [X] CI
  - Travis CI is working for .net CORE
- [ ] Code
  - [ ] Entity Framework
  - [ ] Logger - Log4Net
  - [ ] Angular.js
  - [ ] Caching and Redis ?
  - [ ] SQL Scripts
  - [ ] Unit Tests
  - [ ] Moq
  - [ ] DI/ IOC -> Unity?
- [ ] Performance Tests ?
- [X] Static Code Analysis
  - Enabled [style cop analyzer](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/Configuration.md)
- [X] Docker Scripts ?
- [ ] Front-End Build Process ?
- [ ] CSS Preprocessor
- [ ] JS minify and Bundle
- [ ] Twitter Bootstrap
- [ ] SQL Server Local DB
- [X] Name Generator?
  - use this API https://uinames.com/api/?amount=25&region=united+states
- [ ] Scripts to deploy to Azure?
- [ ] Usage Documentation
- [ ] Tradeoffs
