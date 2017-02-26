using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeopleSearch.Models;
using PeopleSearch.Models.V1;

namespace PeopleSearch.Controllers.API.V1
{
    /// <summary>
    /// 
    /// </summary>
    public class PeopleAPIController : Controller
    {
        private readonly ILogger logger;

        public PeopleAPIController(ILogger<PeopleAPIController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/api/people/generate/{number}")]
        public IEnumerable<string> GenerateRandomPeople(int number)
        {
            return Enumerable.Range(0, number).Select(p => p.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/api/people")]
        public string CreatePerson([FromBody]Person person)
        { 
            logger.LogInformation(string.Format("{0}, {1}, {2}", person.FirstName, person.LastName, person.City));
            return "OK";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/api/people")]
        public IEnumerable<Person> GetAllPeople([FromQuery]PersonQueryParameter parameters)
        {
            return new List<Person>() 
            {
                new Person() { FirstName="test1", LastName="test1", City="test1" },
                new Person() { FirstName="test2", LastName="test2", City="test2" },
                new Person() { FirstName="test3", LastName="test3", City="test3" }
            };
        }
    }
}
