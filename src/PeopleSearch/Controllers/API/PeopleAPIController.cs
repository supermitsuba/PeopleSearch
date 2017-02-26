using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleSearch.Models;

namespace PeopleSearch.Controllers.API
{
    /// <summary>
    /// 
    /// </summary>
    public class PeopleAPIController : Controller
    {
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
        public HttpResponseMessage CreatePerson(Person person)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
               Content = new StringContent("OK")      
            };
        }
    }
}
