using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PeopleSearch.Controllers.API.V1;
using Xunit;

namespace PeopleSearch.Tests.Controllers
{
    public class PeopleAPIController_Tests
    {
        [Fact]
        public void Constructor_API_ThrowExceptionIfNullLogger() 
        {
            var database = new Mock<DataHandler>();

            var result = Assert.Throws<NullReferenceException>(() => { 
                var subject = new PeopleAPIController(null, database.Object);
            });
        }

        [Fact]
        public void Constructor_API_ThrowExceptionIfNullDataHandler() 
        {
            var logger = new Mock<ILogger<PeopleAPIController>>();

            var result = Assert.Throws<NullReferenceException>(() => { 
                var subject = new PeopleAPIController(logger.Object, null);
            });
        }

        [Fact]
        public void GenerateRandomPeople_API_Ok() 
        {
            var logger = new Mock<ILogger<PeopleAPIController>>();
            var database = new Mock<DataHandler>();

            database.Setup(p => p.GenerateUsers(1))
                    .Returns(new List<PeopleSearch.Data.Models.Person>());
            var subject = new PeopleAPIController(logger.Object, database.Object);
            var result = subject.GenerateRandomPeople(1) as OkObjectResult;

            Assert.Equal(result.StatusCode, 200);
        }

        [Fact]
        public void GenerateRandomPerson_API_ThrowException() 
        {
            var logger = new Mock<ILogger<PeopleAPIController>>();
            var database = new Mock<DataHandler>();

            database.Setup(p => p.GenerateUsers(1))
                    .Throws(new Exception());
            var subject = new PeopleAPIController(logger.Object, database.Object);
            var result = subject.GenerateRandomPeople(1) as StatusCodeResult;

            Assert.Equal(result.StatusCode, 500);
        }

        [Fact]
        public void CreatePerson_API_Ok() 
        {
            var logger = new Mock<ILogger<PeopleAPIController>>();
            var database = new Mock<DataHandler>();

            var person = new PeopleSearch.Models.V1.Person();

            database.Setup(p => p.SavePerson( person ));
            var subject = new PeopleAPIController(logger.Object, database.Object);
            var result = subject.CreatePerson(person) as OkResult;

            Assert.Equal(result.StatusCode, 200);
        }

        [Fact]
        public void CreatePerson_API_ThrowException() 
        {
            var logger = new Mock<ILogger<PeopleAPIController>>();
            var database = new Mock<DataHandler>();

            var person = new PeopleSearch.Models.V1.Person();

            database.Setup(p => p.SavePerson(person))
                    .Throws(new Exception());
            var subject = new PeopleAPIController(logger.Object, database.Object);
            var result = subject.CreatePerson(person) as StatusCodeResult;

            Assert.Equal(result.StatusCode, 500);
        }


        [Fact]
        public void GetAllPeople_API_Ok() 
        {
            var logger = new Mock<ILogger<PeopleAPIController>>();
            var database = new Mock<DataHandler>();

            var parameter = new PeopleSearch.Models.V1.PersonQueryParameter()
            {
                 Delay = 0
            };

            database.Setup(p => p.GetAllUsers(parameter))
                    .Returns(new List<PeopleSearch.Models.V1.Person>());
            var subject = new PeopleAPIController(logger.Object, database.Object);
            var result = subject.GetAllPeople(parameter);
            result.Wait();

            Assert.Equal((result.Result as OkObjectResult).StatusCode, 200);
        }

        [Fact]
        public void GetAllPeople_API_DelayWorks() 
        {
            var logger = new Mock<ILogger<PeopleAPIController>>();
            var database = new Mock<DataHandler>();

            var parameter = new PeopleSearch.Models.V1.PersonQueryParameter()
            {
                 Delay = 2
            };

            var stopwatch = new Stopwatch();

            database.Setup(p => p.GetAllUsers(parameter))
                    .Returns(new List<PeopleSearch.Models.V1.Person>());
            var subject = new PeopleAPIController(logger.Object, database.Object);
            stopwatch.Start();
            var result = subject.GetAllPeople(parameter);
            result.Wait();
            stopwatch.Stop();

            Assert.True(stopwatch.ElapsedMilliseconds > 2000);
        }

        [Fact]
        public void GetAllPeople_API_ThrowException() 
        {
            var logger = new Mock<ILogger<PeopleAPIController>>();
            var database = new Mock<DataHandler>();

            var parameter = new PeopleSearch.Models.V1.PersonQueryParameter()
            {
                 Delay = 0
            };

            database.Setup(p => p.GetAllUsers(parameter))
                    .Throws(new Exception());
            var subject = new PeopleAPIController(logger.Object, database.Object);
            var result = subject.GetAllPeople(parameter);
            result.Wait();

            Assert.Equal((result.Result as StatusCodeResult).StatusCode, 500);
        }
    }
}
