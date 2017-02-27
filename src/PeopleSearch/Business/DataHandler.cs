
using System.Collections.Generic;
using System.Threading.Tasks;
using PeopleSearch.Models.V1;

public abstract class DataHandler
{
    protected DataHandler successor;
    public void SetSuccessor(DataHandler successor)
    {
        this.successor = successor;
    }

    public abstract IEnumerable<Person> GetAllUsers(PersonQueryParameter parameters);
    
    public abstract PeopleSearch.Data.Models.Person SavePerson(Person person);

    public abstract IEnumerable<PeopleSearch.Data.Models.Person> GenerateUsers(int number);
}