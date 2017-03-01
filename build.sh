#!/usr/bin/env bash

cd src/PeopleSearch
dotnet restore && dotnet build **/project.json

cd ../../test/PeopleSearch.Tests
dotnet restore && dotnet test ../PeopleSearch.Tests/ 