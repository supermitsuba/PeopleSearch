
namespace PeopleSearch.Models.V1
{
    public class PersonQueryParameter
    {
        public string Prefix { get; set; } 
        public int Delay { get; set; }
        public int Limit { get; set; } 
        public int Offset { get; set; }
    }
}