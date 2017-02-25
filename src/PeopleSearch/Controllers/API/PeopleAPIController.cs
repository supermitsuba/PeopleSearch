using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

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
    }
}
