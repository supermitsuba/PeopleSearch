using System.Collections.Generic;

namespace PeopleSearch.Data.Models 
{
    /// <summary>
    /// 
    /// </summary>   
    public class Interest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Category { get; set; }

        /// <summary>
        /// /// 
        /// </summary>
        /// <returns></returns>
        public string InterestId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int PersonId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Person Person { get; set; }
    }
}