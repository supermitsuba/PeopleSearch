
using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures;
using Microsoft.Extensions.Caching.Memory;
using PeopleSearch;
using PeopleSearch.Data.Models;

public class TrieHandler : DataHandler
{
    private Trie<char, Person> allPeople;
    private IMemoryCache cache;

    public TrieHandler(IMemoryCache cache)
    {
        this.cache = cache;

        if (PeopleSearch.Data.Models.Person.Count() > 100000)
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
        if (!cache.TryGetValue("Trie", out allPeople))
        {            
            allPeople = new DataStructures.Trie<char, Person>();
            foreach (var p in Person.GetAllUsers())
            {
                allPeople.Add(p.FirstName.ToLower(), p);
                allPeople.Add(p.LastName.ToLower(), p);
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions();
            cache.Set("Trie", allPeople);
        }
    }

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

    public override PeopleSearch.Data.Models.Person SavePerson(PeopleSearch.Models.V1.Person person)
    {
        if (successor == null) throw new NullReferenceException("There is no Successor to complete the request.");

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

    public override IEnumerable<PeopleSearch.Data.Models.Person> GenerateUsers(int number)
    {
        if (successor == null) throw new NullReferenceException("There is no Successor to complete the request.");

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