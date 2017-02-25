using System.Collections.Generic;

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
        public int Id { get; set; }

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
        public string State { get; set; }

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
        public string PictureUrl { get; set; }
    }
}