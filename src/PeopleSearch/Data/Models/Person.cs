using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PeopleSearch.Models.V1;

namespace PeopleSearch.Data.Models 
{
    /// <summary>
    /// 
    /// </summary>   
    public class Person
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int PersonId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Address1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Address2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public State AddressState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Zip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Age { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Interests { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string PictureUrl { get; set; }

        public static async Task SavePerson(PeopleSearch.Models.V1.Person person)
        {
            var db = new PersonSearchingContext();

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
                    PictureUrl = person.PictureUrl,
                    Interests = person.Interests
                    // TODO: add Interest conversion function
                };
                
                db.People.Add(newPerson);
                await db.SaveChangesAsync();
        }

        internal static IEnumerable<PeopleSearch.Models.V1.Person> GetAllUsers(PersonQueryParameter parameters)
        {
            var db = new PeopleSearch.Data.PersonSearchingContext();
            var query = db.People.AsQueryable();
            if (!string.IsNullOrEmpty(parameters.Prefix))
            {
                parameters.Prefix = parameters.Prefix.ToLower();
                query = query.Where (p => p.FirstName.ToLower().StartsWith(parameters.Prefix) || p.LastName.ToLower().StartsWith(parameters.Prefix));
            }

            var limit = 0;
            if (parameters.Limit > 0 && parameters.Limit < Constants.DEFAULT_LIMIT) 
            {
                limit = parameters.Limit;
                query = query.Take (parameters.Limit);
            }
            else
            {
                limit = Constants.DEFAULT_LIMIT;
                query = query.Take (Constants.DEFAULT_LIMIT);
            }

            if (parameters.Offset > -1) 
            {
                query = query.Skip (parameters.Offset*limit);
            }
            else
            {
                query = query.Skip (Constants.DEFAULT_OFFSET*limit);
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
                Interests = p.Interests,
                AddressState = p.AddressState.ToString()  
            }).ToList();
        }

        public static async Task<List<Person>> GenerateUsers(int number)
        {
            var names = new List<PeopleSearch.Models.V1.NameApiResult>();
            var people = new List<PeopleSearch.Data.Models.Person>(); 
            var db = new PeopleSearch.Data.PersonSearchingContext();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://uinames.com/");
                var results = await client.GetStringAsync("api/?amount="+number+"&region=united+states");
                names = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PeopleSearch.Models.V1.NameApiResult>>(results);
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
            return people;
        }
    }
}