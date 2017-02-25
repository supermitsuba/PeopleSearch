#!/usr/bin/env bash

cd src/PeopleSearch.Data
dotnet restore && dotnet build **/project.json
cd ../PeopleSearch.Data.Tests
dotnet restore && dotnet test ../PeopleSearch.Data.Tests/

cd ../PeopleSearch
dotnet restore && dotnet build **/project.json
cd ../PeopleSearch.Tests
dotnet restore && dotnet test ../PeopleSearch.Tests/ 