using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PeopleSearch.Models.V1
{
    /// <summary>
    /// The import options for the view
    /// </summary>   
    public class ImportViewModel
    {
        /// <summary>
        /// The number of users to import
        /// </summary>
        /// <returns></returns>
        [Required]
        [Range(1, 100)]
        public int NumberOfUsers { get; set; }
    }
}