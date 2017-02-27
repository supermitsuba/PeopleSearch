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
                var db = new PeopleSearch.Data.PersonSearchingContext();

                var newPerson = new Data.Models.Person()
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Address1 = person.Address1,
                    Address2 = person.Address2,
                    AddressState = Data.Models.State.MI, // TODO: person.AddressState need a conversion function
                    Zip = person.Zip,
                    City = person.City,
                    Age = person.Age,
                    PictureUrl = person.PictureUrl
                    // TODO: add Interest conversion function
                };
                
                db.People.Add(newPerson);
                await db.SaveChangesAsync();
                
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
                await CreateUsers(person.NumberOfUsers);
                return RedirectToAction("Index");
            }

            return View(person);
        }

        public async Task CreateUsers(int number)
        {
            var names = new List<NameApiResult>();
            var people = new List<PeopleSearch.Data.Models.Person>(); 
            var db = new PeopleSearch.Data.PersonSearchingContext();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://uinames.com/");
                var results = await client.GetStringAsync("api/?amount="+number+"&region=united+states");
                names = Newtonsoft.Json.JsonConvert.DeserializeObject<List<NameApiResult>>(results);
            }

            foreach (var n in names)
            {
                var temp = new PeopleSearch.Data.Models.Person()
                {
                    FirstName = n.FirstName,
                    LastName = n.LastName,
                    Address1 = string.Empty,
                    Address2 = string.Empty,
                    City = string.Empty,
                    AddressState = PeopleSearch.Data.Models.State.MI,
                    Zip = 48223,
                    Age = 4,
                    PictureUrl = "http://www.google.com/"
                };
                db.Add(temp);
                people.Add(temp);
            }
            
            await db.SaveChangesAsync();
        }
    }
}
