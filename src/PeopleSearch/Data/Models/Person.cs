namespace PeopleSearch.Data.Models
{
    /// <summary>
    /// A representation of a Person's profile
    /// </summary>   
    public class Person
    {
        /// <summary>
        /// The Id of the person
        /// </summary>
        /// <returns></returns>
        public int PersonId { get; set; }

        /// <summary>
        /// First name of person
        /// </summary>
        /// <returns></returns>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of person
        /// </summary>
        /// <returns></returns>
        public string LastName { get; set; }

        /// <summary>
        /// Address of person
        /// </summary>
        /// <returns></returns>
        public string Address1 { get; set; }

        /// <summary>
        /// More space for address
        /// </summary>
        /// <returns></returns>
        public string Address2 { get; set; }

        /// <summary>
        /// Ciry of where the person lives
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }

        /// <summary>
        /// State where the person lives
        /// </summary>
        /// <returns></returns>
        public State AddressState { get; set; }

        /// <summary>
        /// zip code of the person
        /// </summary>
        /// <returns></returns>
        public int Zip { get; set; }

        /// <summary>
        /// The Age of the person
        /// </summary>
        /// <returns></returns>
        public int Age { get; set; }

        /// <summary>
        /// A free form text blob of all the interests of the person
        /// </summary>
        /// <returns></returns>
        public string Interests { get; set; }

        /// <summary>
        /// A url of the person's picture
        /// </summary>
        /// <returns></returns>
        public string PictureUrl { get; set; }
    }
}