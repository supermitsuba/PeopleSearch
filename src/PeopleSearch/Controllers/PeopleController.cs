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
            ViewData["SearchPage"] = true;
            
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
            ViewData["CreatePage"] = true;
            return View();
        }

        [HttpPost]
        [Route("[controller]/create")]
        public IActionResult Create(Models.V1.Person person)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns> <summary>
        [HttpGet]
        [Route("[controller]/import")]
        public IActionResult Import()
        {
            ViewData["ImportPage"] = true;
            return View();
        }

        [HttpPost]
        [Route("[controller]/import")]
        public IActionResult Import(Models.V1.ImportViewModel person)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }
    }
}
