using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeopleSearch.Models.V1
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
        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns> <summary> <summary>
        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns> <summary>
        [StringLength(25)]
        [Required]
        public string Address1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [StringLength(25)]
        public string Address2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [StringLength(22)]
        [Required]
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [StringLength(2)]
        [Required]
        public string AddressState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns> <summary>
        [Range(10000, 99999)]
        [Required]
        public int Zip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Range(18, 130)]
        [Required]
        public int Age { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Interests { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Url]
        [Required]
        public string PictureUrl { get; set; }
    }
}