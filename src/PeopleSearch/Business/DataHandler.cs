using System.Collections.Generic;
using PeopleSearch.Models.V1;

/// <summary>
/// 
/// </summary>
public abstract class DataHandler
{
    /// <summary>
    /// 
    /// </summary>
    protected DataHandler successor;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="successor"></param>
    public void SetSuccessor(DataHandler successor)
    {
        this.successor = successor;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public abstract IEnumerable<Person> GetAllUsers(PersonQueryParameter parameters);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    public abstract PeopleSearch.Data.Models.Person SavePerson(Person person);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public abstract IEnumerable<PeopleSearch.Data.Models.Person> GenerateUsers(int number);
}