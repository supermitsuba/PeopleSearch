#!/usr/bin/env bash

cd src/PeopleSearch
dotnet restore && dotnet build **/project.json
dotnet ef migrations add MyFirstMigration
dotnet ef database update

cd ../PeopleSearch.Tests
dotnet restore && dotnet test ../PeopleSearch.Tests/ 