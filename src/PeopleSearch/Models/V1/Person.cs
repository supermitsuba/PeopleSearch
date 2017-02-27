using System.ComponentModel.DataAnnotations;

namespace PeopleSearch.Models.V1
{
    /// <summary>
    /// The person to show in the Views.  Not the person saved to the database.
    /// </summary>   
    public class Person
    {
        /// <summary>
        /// First name of the person
        /// </summary>
        /// <returns></returns>
        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the person
        /// </summary>
        /// <returns></returns>
        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        /// <summary>
        /// Address of the person
        /// </summary>
        /// <returns></returns>
        [StringLength(25)]
        [Required]
        public string Address1 { get; set; }

        /// <summary>
        /// more space for address
        /// </summary>
        /// <returns></returns>
        [StringLength(25)]
        public string Address2 { get; set; }

        /// <summary>
        /// City of the person
        /// </summary>
        /// <returns></returns>
        [StringLength(22)]
        [Required]
        public string City { get; set; }

        /// <summary>
        /// State of the person
        /// </summary>
        /// <returns></returns>
        [StringLength(2)]
        [Required]
        public string AddressState { get; set; }

        /// <summary>
        /// Zip of the person
        /// </summary>
        /// <returns></returns>
        [Range(10000, 99999)]
        [Required]
        public int Zip { get; set; }

        /// <summary>
        /// Age of the person
        /// </summary>
        /// <returns></returns>
        [Range(18, 130)]
        [Required]
        public int Age { get; set; }

        /// <summary>
        /// Interests of the person
        /// </summary>
        /// <returns></returns>
        public string Interests { get; set; }

        /// <summary>
        /// Picture of the person
        /// </summary>
        /// <returns></returns>
        [Url]
        [Required]
        public string PictureUrl { get; set; }
    }
}