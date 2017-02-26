FROM microsoft/dotnet:1.0.3-sdk-projectjson 

RUN apt update && apt install -y git
RUN git clone https://github.com/supermitsuba/PeopleSearch.git /var/www/PeopleSearch

WORKDIR /var/www/PeopleSearch
RUN ./build.sh

WORKDIR /var/www/PeopleSearch/src/PeopleSearch

EXPOSE 8000

ENTRYPOINT ["dotnet", "run", "--server.urls=http://0.0.0.0:8000/"]