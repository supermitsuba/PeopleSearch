using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PeopleSearch.Models.V1;

namespace PeopleSearch.Controllers.API.V1
{
    /// <summary>
    /// 
    /// </summary>
    public class PeopleAPIController : Controller
    {
        private readonly ILogger logger;
        private readonly DataHandler handler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public PeopleAPIController(ILogger<PeopleAPIController> logger, DataHandler handler)
        {
            this.logger = logger;
            this.handler = handler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/api/people/generate/{number}")]
        public async Task<IEnumerable<string>> GenerateRandomPeople(int number)
        {
            return handler.GenerateUsers(number).Select(p => p.FirstName + " " + p.LastName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/api/people")]
        public async Task<string> CreatePerson([FromBody]Person person)
        { 
            handler.SavePerson(person);
            return "OK";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/api/people")]
        public async Task<IEnumerable<Person>> GetAllPeople([FromQuery]PersonQueryParameter parameters)
        {
            await Task.Delay(parameters.Delay * 1000);

            return handler.GetAllUsers(parameters);
                
        }
    }
}
