using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public PeopleAPIController(ILogger<PeopleAPIController> logger)
        {
            this.logger = logger;
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

            return people.Select(p => p.FirstName + " " + p.LastName);
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

            var db = new PeopleSearch.Data.PersonSearchingContext();
            var query = db.People.AsQueryable();
            if (!string.IsNullOrEmpty(parameters.Prefix))
            {
                parameters.Prefix = parameters.Prefix.ToLower();
                query = query.Where (p => p.FirstName.ToLower().StartsWith(parameters.Prefix) || p.LastName.ToLower().StartsWith(parameters.Prefix));
            }

            if (parameters.Offset > -1) 
            {
                query = query.Skip (parameters.Offset);
            }
            else
            {
                query = query.Skip (Constants.DEFAULT_OFFSET);
            }

            if (parameters.Limit > 0 && parameters.Limit < Constants.DEFAULT_LIMIT) 
            {
                query = query.Take (parameters.Limit);
            }
            else
            {
                query = query.Take (Constants.DEFAULT_LIMIT);
            }

            return query.Select(p => new PeopleSearch.Models.V1.Person()
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Address1 = p.Address1,
                Address2 = p.Address2,
                Zip = p.Zip,
                City = p.City,
                Age = p.Age,
                PictureUrl = p.PictureUrl,
                Interests = p.Interests.Select(r => r.Category),
                AddressState = p.AddressState.ToString()  
            }).ToList();
                
        }
    }
}
