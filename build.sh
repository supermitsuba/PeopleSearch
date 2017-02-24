#!/usr/bin/env bash
cd src/PeopleSearch
dotnet restore && dotnet build **/project.json
dotnet test ../PeopleSearch.Tests/