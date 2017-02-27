
using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures;
using Microsoft.Extensions.Caching.Memory;
using PeopleSearch;
using PeopleSearch.Data;
using PeopleSearch.Data.Models;

/// <summary>
/// This class is to take a list of persons and make it faster to look them up.  A Trie is a tree like structure that makes a search as fast as O(searchTerm).
/// </summary>
public class TrieHandler : DataHandler
{
    private Trie<char, Person> allPeople;
    private IMemoryCache cache;
    private readonly PersonSearchingContext db;

    /// <summary>
    /// This initializes the TrieHandler class
    /// </summary>
    /// <param name="cache">This is for caching the Trie for further use.</param>
    /// <param name="db">This is for looking up the database to see if it is more efficient to use the database instead.</param>
    public TrieHandler(IMemoryCache cache, PersonSearchingContext db)
    {
        this.cache = cache;
        this.db = db;

        var countTask = db.Count();
        countTask.Wait();

        if (countTask.Result > 100000)
        {
            allPeople = null;
        }
        else
        {
            GetTrieCache();
        }
    }

    private void GetTrieCache()
    {
        if (!cache.TryGetValue("Trie", out allPeople)) //see if there is not a Trie
        {            
            allPeople = new DataStructures.Trie<char, Person>();
            var allUserTask = db.GetAllUsers(); // get a list of all users.
            allUserTask.Wait();

            foreach (var p in allUserTask.Result) // add all users to the Trie
            {
                allPeople.Add(p.FirstName.ToLower(), p);
                allPeople.Add(p.LastName.ToLower(), p);
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions();
            cache.Set("Trie", allPeople); // save Trie to cache
        }
    }

    /// <summary>
    /// Gets all users matching the paramref name="parameters"
    /// </summary>
    /// <param name="parameters">This contains the prefix</param>
    /// <returns>Returns a list of all users where the first or last name matches the search term.</returns>
    public override IEnumerable<PeopleSearch.Models.V1.Person> GetAllUsers(PeopleSearch.Models.V1.PersonQueryParameter parameters)
    {
        if (successor == null) throw new NullReferenceException("There is no Successor to complete the request.");

        if (allPeople == null)
        {
            return successor.GetAllUsers(parameters);
        }
        else
        {
            var results = allPeople.Suffixes(parameters.Prefix.ToLower()).Select(p => p.Value);
            var limit = 0;
            if (parameters.Limit > 0 && parameters.Limit < Constants.DEFAULT_LIMIT)
            {
                limit = parameters.Limit;
                results = results.Take(parameters.Limit);
            }
            else
            {
                limit = Constants.DEFAULT_LIMIT;
                results = results.Take(Constants.DEFAULT_LIMIT);
            }

            if (parameters.Offset > -1)
            {
                results = results.Skip(parameters.Offset * limit);
            }
            else
            {
                results = results.Skip(Constants.DEFAULT_OFFSET * limit);
            }

            return results.Select(p => new PeopleSearch.Models.V1.Person()
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
    }

    /// <summary>
    /// Saves the MVC user into the Trie
    /// </summary>
    /// <param name="person">an MVC person</param>
    /// <returns>returns the user that was saved from the successor</returns>
    public override PeopleSearch.Data.Models.Person SavePerson(PeopleSearch.Models.V1.Person person)
    {
        if (successor == null) 
        {
            throw new NullReferenceException("There is no Successor to complete the request.");
        }

        if (allPeople == null)
        {
            return successor.SavePerson(person);
        }
        else
        {
            var p = successor.SavePerson(person);
            allPeople.Add(p.FirstName.ToLower(), p);
            allPeople.Add(p.LastName.ToLower(), p);
            return p;
        }
    }

    /// <summary>
    /// Generates a set of users and stores them in the Trie
    /// </summary>
    /// <param name="number">Number of users to store in the trie</param>
    /// <returns>A list of users saved into the database and Trie</returns>
    public override IEnumerable<PeopleSearch.Data.Models.Person> GenerateUsers(int number)
    {
        if (successor == null) 
        {
            throw new NullReferenceException("There is no Successor to complete the request.");
        }

        if (allPeople == null) //change to configuration
        {
            return successor.GenerateUsers(number);
        }
        else
        {
            var people = successor.GenerateUsers(number);
            foreach (var p in people)
            {
                allPeople.Add(p.FirstName.ToLower(), p);
                allPeople.Add(p.LastName.ToLower(), p);
            }

            return people;
        }
    }
}