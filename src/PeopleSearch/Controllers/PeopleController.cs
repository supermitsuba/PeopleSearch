using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PeopleSearch.Models.V1;

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
        public async Task<IActionResult> Create(Models.V1.Person person)
        {
            if (ModelState.IsValid)
            {
                PeopleSearch.Data.Models.Person.SavePerson(person);                
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
        public async Task<IActionResult> Import(Models.V1.ImportViewModel person)
        {
            if (ModelState.IsValid)
            {
                await PeopleSearch.Data.Models.Person.GenerateUsers(person.NumberOfUsers);
                return RedirectToAction("Index");
            }

            return View(person);
        }
    }
}
