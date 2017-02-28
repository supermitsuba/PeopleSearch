
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using PeopleSearch;
using PeopleSearch.Data;

/// <summary>
/// 
/// </summary>
public class DatabaseHandler : DataHandler
{
    private readonly PersonSearchingContext db;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public DatabaseHandler(PersonSearchingContext context)
    {
        this.db = context;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override IEnumerable<PeopleSearch.Models.V1.Person> GetAllUsers(PeopleSearch.Models.V1.PersonQueryParameter parameters)
    {
        try
        {
            var query = db.People.AsQueryable();
            if (!string.IsNullOrEmpty(parameters.Prefix))
            {
                parameters.Prefix = parameters.Prefix.ToLower();
                query = query.Where(p => p.FirstName.ToLower().StartsWith(parameters.Prefix) || p.LastName.ToLower().StartsWith(parameters.Prefix));
            }

            var limit = 0;
            if (parameters.Limit > 0 && parameters.Limit < Constants.DEFAULT_LIMIT)
            {
                limit = parameters.Limit;
                query = query.Take(parameters.Limit);
            }
            else
            {
                limit = Constants.DEFAULT_LIMIT;
                query = query.Take(Constants.DEFAULT_LIMIT);
            }

            if (parameters.Offset > -1)
            {
                query = query.Skip(parameters.Offset * limit);
            }
            else
            {
                query = query.Skip(Constants.DEFAULT_OFFSET * limit);
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
        catch (Exception exc)
        {
            if (successor == null) throw exc;

            return successor.GetAllUsers(parameters);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    public override PeopleSearch.Data.Models.Person SavePerson(PeopleSearch.Models.V1.Person person)
    {
        try
        {
            var newPerson = new PeopleSearch.Data.Models.Person()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Address1 = person.Address1,
                Address2 = person.Address2,
                AddressState = PeopleSearch.Data.Models.State.MI, // TODO: person.AddressState need a conversion function
                Zip = person.Zip,
                City = person.City,
                Age = person.Age,
                PictureUrl = person.PictureUrl,
                Interests = person.Interests
            };

            db.People.Add(newPerson);
            var task = db.SaveChangesAsync();
            task.Wait();

            return newPerson;
        }
        catch (Exception exc)
        {
            if (successor == null) throw exc;

            return successor.SavePerson(person);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public override IEnumerable<PeopleSearch.Data.Models.Person> GenerateUsers(int number)
    {
        try
        {
            var names = new List<PeopleSearch.Models.V1.NameApiResult>();
            var people = new List<PeopleSearch.Data.Models.Person>();
            var rnd = new Random();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://uinames.com/");
                var results = client.GetStringAsync("api/?amount=" + number + "&region=united+states");
                results.Wait();
                names = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PeopleSearch.Models.V1.NameApiResult>>(results.Result);
            }

            foreach (var n in names)
            {
                var temp = new PeopleSearch.Data.Models.Person()
                {
                    FirstName = n.FirstName,
                    LastName = n.LastName,
                    Address1 = "1352 Random St.",
                    Address2 = string.Empty,
                    City = "RandomCity",
                    AddressState = GetRandomState(rnd),
                    Zip = rnd.Next(10000, 99999), //this isnt correct, but it provides something
                    Age = rnd.Next(0, 100),
                    PictureUrl = "http://jordenlowe.com/logo.jpg"
                };

                db.Add(temp);
                people.Add(temp);
            }

            db.SaveChangesAsync().Wait();
            return people;
        }
        catch (Exception exc)
        {
            if (successor == null) 
            {
                throw exc;
            }

            return successor.GenerateUsers(number);
        }
    }

    private PeopleSearch.Data.Models.State GetRandomState(Random rnd)
    {
        Dictionary<string, string> states = new Dictionary<string, string>();
        states.Add("AL", "Alabama");
        states.Add("AK", "Alaska");
        states.Add("AZ", "Arizona");
        states.Add("AR", "Arkansas");
        states.Add("CA", "California");
        states.Add("CO", "Colorado");
        states.Add("CT", "Connecticut");
        states.Add("DE", "Delaware");
        states.Add("DC", "District of Columbia");
        states.Add("FL", "Florida");
        states.Add("GA", "Georgia");
        states.Add("HI", "Hawaii");
        states.Add("ID", "Idaho");
        states.Add("IL", "Illinois");
        states.Add("IN", "Indiana");
        states.Add("IA", "Iowa");
        states.Add("KS", "Kansas");
        states.Add("KY", "Kentucky");
        states.Add("LA", "Louisiana");
        states.Add("ME", "Maine");
        states.Add("MD", "Maryland");
        states.Add("MA", "Massachusetts");
        states.Add("MI", "Michigan");
        states.Add("MN", "Minnesota");
        states.Add("MS", "Mississippi");
        states.Add("MO", "Missouri");
        states.Add("MT", "Montana");
        states.Add("NE", "Nebraska");
        states.Add("NV", "Nevada");
        states.Add("NH", "New Hampshire");
        states.Add("NJ", "New Jersey");
        states.Add("NM", "New Mexico");
        states.Add("NY", "New York");
        states.Add("NC", "North Carolina");
        states.Add("ND", "North Dakota");
        states.Add("OH", "Ohio");
        states.Add("OK", "Oklahoma");
        states.Add("OR", "Oregon");
        states.Add("PA", "Pennsylvania");
        states.Add("RI", "Rhode Island");
        states.Add("SC", "South Carolina");
        states.Add("SD", "South Dakota");
        states.Add("TN", "Tennessee");
        states.Add("TX", "Texas");
        states.Add("UT", "Utah");
        states.Add("VT", "Vermont");
        states.Add("VA", "Virginia");
        states.Add("WA", "Washington");
        states.Add("WV", "West Virginia");
        states.Add("WI", "Wisconsin");
        states.Add("WY", "Wyoming");
        var stateNumber = rnd.Next(0, 49);
        PeopleSearch.Data.Models.State result;
        Enum.TryParse(states.ToArray()[stateNumber].Key, out result);
        return result;
    }
}