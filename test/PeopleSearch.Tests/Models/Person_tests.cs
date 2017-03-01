using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace PeopleSearch.Tests.Models
{
    public class Person_Tests
    {
        [Fact]
        public void Models_InvalidFirstName() 
        {
            var person = new PeopleSearch.Models.V1.Person()
            {
                 LastName = "Lowe",
                 Address1 = "111 Test St.",
                 Address2 = "a",
                 City = "Detroit",
                 AddressState = "MI",
                 Zip = 48223,
                 Age = 33,
                 Interests = "Programming",
                 PictureUrl = "http://jordenlowe.com/logo.jpg"
            };

            Assert.Equal(1, ValidateModel(person).Count);
        }

        [Fact]
        public void Models_InvalidLastName() 
        {
            var person = new PeopleSearch.Models.V1.Person()
            {
                 FirstName = "Lowe",
                 Address1 = "111 Test St.",
                 Address2 = "a",
                 City = "Detroit",
                 AddressState = "MI",
                 Zip = 48223,
                 Age = 33,
                 Interests = "Programming",
                 PictureUrl = "http://jordenlowe.com/logo.jpg"
            };

            Assert.Equal(1, ValidateModel(person).Count);
        }

        [Fact]
        public void Models_InvalidAddress1() 
        {
            var person = new PeopleSearch.Models.V1.Person()
            {
                 LastName = "Lowe",
                 FirstName = "111 Test St.",
                 Address2 = "a",
                 City = "Detroit",
                 AddressState = "MI",
                 Zip = 48223,
                 Age = 33,
                 Interests = "Programming",
                 PictureUrl = "http://jordenlowe.com/logo.jpg"
            };

            Assert.Equal(1, ValidateModel(person).Count);
        }



        [Fact]
        public void Models_InvalidCity() 
        {
            var person = new PeopleSearch.Models.V1.Person()
            {
                 LastName = "Lowe",
                 FirstName = "111 Test St.",
                 Address2 = "a",
                 Address1 = "Detroit",
                 AddressState = "MI",
                 Zip = 48223,
                 Age = 33,
                 Interests = "Programming",
                 PictureUrl = "http://jordenlowe.com/logo.jpg"
            };

            Assert.Equal(1, ValidateModel(person).Count);
        }

        [Fact]
        public void Models_InvalidAddressState() 
        {
            var person = new PeopleSearch.Models.V1.Person()
            {
                 LastName = "Lowe",
                 FirstName = "111 Test St.",
                 Address2 = "a",
                 City = "Detroit",
                 Address1 = "MI",
                 Zip = 48223,
                 Age = 33,
                 Interests = "Programming",
                 PictureUrl = "http://jordenlowe.com/logo.jpg"
            };

            Assert.Equal(1, ValidateModel(person).Count);
        }

        [Fact]
        public void Models_InvalidZip() 
        {
            var person = new PeopleSearch.Models.V1.Person()
            {
                 FirstName = "Jorden",
                 LastName = "Lowe",
                 Address1 = "111 Test St.",
                 Address2 = "a",
                 City = "Detroit",
                 AddressState = "MI",
                 Age = 33,
                 Interests = "Programming",
                 PictureUrl = "http://jordenlowe.com/logo.jpg"
            };

            Assert.Equal(1, ValidateModel(person).Count);
        }

        [Fact]
        public void Models_InvalidAge() 
        {
            var person = new PeopleSearch.Models.V1.Person()
            {
                 FirstName = "Jorden",
                 LastName = "Lowe",
                 Address1 = "111 Test St.",
                 Address2 = "a",
                 City = "Detroit",
                 AddressState = "MI",
                 Zip = 48223,
                 Interests = "Programming",
                 PictureUrl = "http://jordenlowe.com/logo.jpg"
            };

            Assert.Equal(1, ValidateModel(person).Count);
        }


        [Fact]
        public void Models_InvalidPictureUrl() 
        {
            var person = new PeopleSearch.Models.V1.Person()
            {
                 FirstName = "Jorden",
                 LastName = "Lowe",
                 Address1 = "111 Test St.",
                 Address2 = "a",
                 City = "Detroit",
                 AddressState = "MI",
                 Zip = 48223,
                 Age = 33,
                 Interests = "Programming"
            };

            Assert.Equal(1, ValidateModel(person).Count);
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}