using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PeopleSearch.Models.V1
{
    /// <summary>
    /// 
    /// </summary>   
    public class ImportViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Required]
        [Range(1, 100)]
        public int NumberOfUsers { get; set; }
    }
}