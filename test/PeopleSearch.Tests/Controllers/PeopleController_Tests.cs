using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PeopleSearch.Controllers;
using PeopleSearch.Settings;
using Xunit;

namespace PeopleSearch.Tests.Controllers
{
    public class PeopleController_Tests
    {
        [Fact]
        public void Constructor_ThrowExceptionIfNullLogger() 
        {
            var cache = new Mock<IMemoryCache>();
            var database = new Mock<DataHandler>();
            var settings = new Mock<IOptions<AppSettings>>();

            var result = Assert.Throws<NullReferenceException>(() => { 
                var subject = new PeopleController(null, cache.Object, database.Object, settings.Object);
            });
        }

        [Fact]
        public void Constructor_ThrowExceptionIfNullMemoryCache() 
        {
            var logger = new Mock<ILogger<PeopleController>>();
            var database = new Mock<DataHandler>();
            var settings = new Mock<IOptions<AppSettings>>();

            var result = Assert.Throws<NullReferenceException>(() => { 
                var subject = new PeopleController(logger.Object, null, database.Object, settings.Object);
            });
        }

        [Fact]
        public void Constructor_ThrowExceptionIfNullDataHandler() 
        {
            var cache = new Mock<IMemoryCache>();
            var logger = new Mock<ILogger<PeopleController>>();
            var settings = new Mock<IOptions<AppSettings>>();

            var result = Assert.Throws<NullReferenceException>(() => { 
                var subject = new PeopleController(logger.Object, cache.Object, null, settings.Object);
            });
        }

        [Fact]
        public void Constructor_ThrowExceptionIfNullAppSettings() 
        {
            var cache = new Mock<IMemoryCache>();
            var database = new Mock<DataHandler>();
            var logger = new Mock<ILogger<PeopleController>>();

            var result = Assert.Throws<NullReferenceException>(() => { 
                var subject = new PeopleController(logger.Object, cache.Object, database.Object, null);
            });
        }

        [Fact]
        public void Index_ReturnsView() 
        {
            var cache = new Mock<IMemoryCache>();
            var database = new Mock<DataHandler>();
            var logger = new Mock<ILogger<PeopleController>>();
            var settings = new Mock<IOptions<AppSettings>>();

            var subject = new PeopleController(logger.Object, cache.Object, database.Object, settings.Object);
            var result = subject.Index() as ViewResult;

            Assert.Equal("Index", result.ViewName);
        }

        [Fact]
        public void Create_ReturnsView() 
        {
            var cache = new Mock<IMemoryCache>();
            var database = new Mock<DataHandler>();
            var logger = new Mock<ILogger<PeopleController>>();
            var settings = new Mock<IOptions<AppSettings>>();

            var subject = new PeopleController(logger.Object, cache.Object, database.Object, settings.Object);
            var result = subject.Create() as ViewResult;

            Assert.Equal("Create", result.ViewName);
        }

        [Fact]
        public void Create_PostRediretToIndex() 
        {
            var cache = new Mock<IMemoryCache>();
            var logger = new Mock<ILogger<PeopleController>>();
            var settings = new Mock<IOptions<AppSettings>>();
            var database = new Mock<DataHandler>();

            var person = new PeopleSearch.Models.V1.Person()
            {
                 FirstName = "Jorden",
                 LastName = "Lowe",
                 Address1 = "111 Test St.",
                 Address2 = "",
                 City = "Detroit",
                 AddressState = "Michigan",
                 Zip = 48223,
                 Age = 33,
                 Interests = "Programming",
                 PictureUrl = "http://jordenlowe.com/logo.jpg"
            };

            database.Setup( p => p.SavePerson(person) )
                    .Returns(new Data.Models.Person());
            var subject = new PeopleController(logger.Object, cache.Object, database.Object, settings.Object);
            var result = subject.Create(person);

            Assert.Equal(subject.ModelState.IsValid, true);
        }

        [Fact]
        public void Create_PostThrowsException() 
        {
            var cache = new Mock<IMemoryCache>();
            var logger = new Mock<ILogger<PeopleController>>();
            var settings = new Mock<IOptions<AppSettings>>();
            var database = new Mock<DataHandler>();

            var savePerson = new PeopleSearch.Models.V1.Person();

            database.Setup( p => p.SavePerson(savePerson) )
                    .Throws(new Exception());
            var subject = new PeopleController(logger.Object, cache.Object, database.Object, settings.Object);
            var result = subject.Create(savePerson) as StatusCodeResult;

            Assert.Equal(result.StatusCode, 500);
        }

        [Fact]
        public void Import_ReturnsView() 
        {
            var cache = new Mock<IMemoryCache>();
            var database = new Mock<DataHandler>();
            var logger = new Mock<ILogger<PeopleController>>();
            var settings = new Mock<IOptions<AppSettings>>();

            var subject = new PeopleController(logger.Object, cache.Object, database.Object, settings.Object);
            var result = subject.Import() as ViewResult;

            Assert.Equal("Import", result.ViewName);
        }

        [Fact]
        public void Import_PostThrowsException() 
        {
            var cache = new Mock<IMemoryCache>();
            var logger = new Mock<ILogger<PeopleController>>();
            var settings = new Mock<IOptions<AppSettings>>();
            var database = new Mock<DataHandler>();

            database.Setup( p => p.GenerateUsers(2) )
                    .Throws(new Exception());
            var subject = new PeopleController(logger.Object, cache.Object, database.Object, settings.Object);
            var result = subject.Import(new PeopleSearch.Models.V1.ImportViewModel() { NumberOfUsers=2 }) as StatusCodeResult;

            Assert.Equal(result.StatusCode, 500);
        }
    }
}
