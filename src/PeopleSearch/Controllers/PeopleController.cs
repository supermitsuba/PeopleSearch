using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PeopleSearch.Settings;

namespace PeopleSearch.Controllers
{
    /// <summary>
    /// The purpose of this controller is to provide an MVC interface to people.
    /// </summary>
    public class PeopleController : Controller
    {
        private readonly DataHandler handler;

        private readonly AppSettings appSettings;

        private readonly IMemoryCache cache;

        /// <summary>
        /// Initializes a new instance of the PeopleController class.
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="handler"></param>
        /// <param name="appSettings"></param>
        public PeopleController(IMemoryCache cache, DataHandler handler, IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
            this.cache = cache;
            this.handler = handler;           
        }

        /// <summary>
        /// The link: http://localhost:8000/people
        /// Used to show the initial search page.
        /// </summary>
        /// <returns>returns the view for search.</returns>
        [HttpGet]
        [Route("[controller]")]
        public IActionResult Index()
        {
            ViewData["SearchPage"] = true;
            
            return View();
        }

        /// <summary>
        /// The link: http://localhost:8000/people/create
        /// Used to show the create form page.
        /// </summary>
        /// <returns>returns the view of the create form.</returns>
        [HttpGet]
        [Route("[controller]/create")]
        public IActionResult Create()
        {
            ViewData["CreatePage"] = true; 
            return View();
        }

        /// <summary>
        /// The link: http://localhost:8000/people/create
        /// Used to post a new paramref name="person".
        /// </summary>
        /// <param name="person">The person to create.</param>
        /// <returns>Returns the user to search page if successful.</returns>
        [HttpPost]
        [Route("[controller]/create")]
        public IActionResult Create(Models.V1.Person person)
        {
            if (ModelState.IsValid)
            {
                handler.SavePerson(person);                
                return RedirectToAction("Index");
            }

            return View(person);
        }

        /// <summary>
        /// The link: http://localhost:8000/people/import
        /// A page to import multple people profiles
        /// </summary>
        /// <returns>Returns a view of the import page.</returns>
        [HttpGet]
        [Route("[controller]/import")]
        public IActionResult Import()
        {
            ViewData["ImportPage"] = true;
            return View();
        }

        /// <summary>
        /// The link: http://localhost:8000/people/import
        /// A route to post multiple people.
        /// </summary>
        /// <param name="importViewModel">All the import settings to use for creating a user.</param>
        /// <returns>This will redirect to Search page.</returns>
        [HttpPost]
        [Route("[controller]/import")]
        public IActionResult Import(Models.V1.ImportViewModel importViewModel)
        {
            if (ModelState.IsValid)
            {
                handler.GenerateUsers(importViewModel.NumberOfUsers);
                return RedirectToAction("Index");
            }

            return View(importViewModel);
        }
    }
}
