namespace PeopleSearch.Models.V1
{
    /// <summary>
    /// The View Model for the Search page.
    /// </summary>
    public class PersonQueryParameter
    {
        /// <summary>
        /// The prefix of what to search.
        /// </summary>
        /// <returns></returns>
        public string Prefix { get; set; } 
        
        /// <summary>
        /// The delay to give the search.  5 seconds typically.
        /// </summary>
        /// <returns></returns>
        public int Delay { get; set; }
        
        /// <summary>
        /// Limit the number of records to send.
        /// </summary>
        /// <returns></returns>
        public int Limit { get; set; } 
        
        /// <summary>
        /// The page of the results.
        /// </summary>
        /// <returns></returns>
        public int Offset { get; set; }
    }
}