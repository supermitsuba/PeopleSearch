using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace PeopleSearch.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]")]
    public class PeopleController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}
