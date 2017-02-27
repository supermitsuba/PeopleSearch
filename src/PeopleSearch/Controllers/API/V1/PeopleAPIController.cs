using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeopleSearch.Models.V1;

namespace PeopleSearch.Controllers.API.V1
{
    /// <summary>
    /// The purpose of this controller is to expose an API of people search software.
    /// </summary>
    public class PeopleAPIController : Controller
    {
        private readonly ILogger logger;
        private readonly DataHandler handler;

        /// <summary>
        /// Initializes a new instance of the PeopleAPIController class.
        /// </summary>
        /// <param name="logger">Logger will log values to a file or console.</param>
        /// <param name="handler">Handler takes care of getting the data to return to the user.</param>
        public PeopleAPIController(ILogger<PeopleAPIController> logger, DataHandler handler)
        {
            this.logger = logger;
            this.handler = handler;
        }

        /// <summary>
        /// This API call generates random people
        /// </summary>
        /// <param name="number">The number of people to generate.</param>
        /// <returns>Returns a list of the users generated.</returns>
        [HttpPost]
        [Route("v1/api/people/generate/{number}")]
        public IEnumerable<string> GenerateRandomPeople(int number)
        {
            return handler.GenerateUsers(number).Select(p => p.FirstName + " " + p.LastName);
        }

        /// <summary>
        /// This API call creates a paramref name="person".
        /// </summary>
        /// <param name="person">A model of a paramref name="person".</param>
        /// <returns>Returns if the action was OK or not.</returns>
        [HttpPost]
        [Route("v1/api/people")]
        public string CreatePerson([FromBody]Person person)
        { 
            handler.SavePerson(person);
            return "OK";
        }

        /// <summary>
        /// This API call gets all people in the API, based on a search prefix.  This will
        /// page the results and provide an interface for it.
        /// </summary>
        /// <param name="parameters">These are all the options that the API has to get people.</param>
        /// <returns>Returns a list of all the people that fit the criteria in paramref name="parameters".</returns>
        [HttpGet]
        [Route("v1/api/people")]
        public async Task<IEnumerable<Person>> GetAllPeople([FromQuery]PersonQueryParameter parameters)
        {
            await Task.Delay(parameters.Delay * 1000);
            return handler.GetAllUsers(parameters);
        }
    }
}
