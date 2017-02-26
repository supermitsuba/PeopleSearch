using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace PeopleSearch.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PeopleController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns> <summary>
        [HttpGet]
        [Route("[controller]/create")]
        public IActionResult Create()
        {
            return View();
        }
    }
}
