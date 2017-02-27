using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PeopleSearch.Models.V1;

namespace PeopleSearch.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PeopleController : Controller
    {
        private readonly DataHandler handler;

        private readonly AppSettings appSettings;

        private readonly IMemoryCache cache;

        public PeopleController(IMemoryCache cache, DataHandler handler, IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
            this.cache = cache;
            this.handler = handler;           
        }

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
                handler.SavePerson(person);                
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
                handler.GenerateUsers(person.NumberOfUsers);
                return RedirectToAction("Index");
            }

            return View(person);
        }
    }
}
