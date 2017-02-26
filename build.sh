#!/usr/bin/env bash

cd ../PeopleSearch
dotnet restore && dotnet build **/project.json
cd ../PeopleSearch.Tests
dotnet restore && dotnet test ../PeopleSearch.Tests/ 