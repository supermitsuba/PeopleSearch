using System.Collections.Generic;
using PeopleSearch.Models.V1;

/// <summary>
/// This is an abstract class to create a Chain of Responsibility for getting data.  This could include the following:
/// 1. database
/// 2. distributed cache, like Redis
/// 3. Trie or some optizied data structure
/// 4. more.
/// </summary>
public abstract class DataHandler
{
    /// <summary>
    /// This represents the next handler that could get valid data for the client.
    /// </summary>
    protected DataHandler successor;

    /// <summary>
    /// This is a function to set the next handler incase this one fails to get data.
    /// </summary>
    /// <param name="successor"></param>
    public void SetSuccessor(DataHandler successor)
    {
        this.successor = successor;
    }

    /// <summary>
    /// Gets all users in the database, based on PersonQueryParameters
    /// </summary>
    /// <param name="parameters">Parameters to narrow which people to retrieve.</param>
    /// <returns>A list of users to satisfy the paramref name="parameters".</returns>
    public abstract IEnumerable<Person> GetAllUsers(PersonQueryParameter parameters);
    
    /// <summary>
    /// Saves a MVC person into the database.  This function translate between them.
    /// </summary>
    /// <param name="person">An MVC person</param>
    /// <returns>The database person that was saved.</returns>
    public abstract PeopleSearch.Data.Models.Person SavePerson(Person person);

    /// <summary>
    /// Generates a list of users based on web services and other means.
    /// </summary>
    /// <param name="number">The number of users to generate</param>
    /// <returns>List of users that were generated.</returns>
    public abstract IEnumerable<PeopleSearch.Data.Models.Person> GenerateUsers(int number);
}