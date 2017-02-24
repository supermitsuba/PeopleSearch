#!/usr/bin/env bash
cd src/PeopleSearch
dotnet restore && dotnet build **/project.json
cd ../PeopleSearch.Tests
dotnet restore && ddotnet test ../PeopleSearch.Tests/