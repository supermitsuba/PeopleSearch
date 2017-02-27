using Newtonsoft.Json;

namespace PeopleSearch.Models.V1
{
    /// <summary>
    /// Used to deserialize the name generate API for importing.
    /// </summary>   
    public class NameApiResult
    {
        /// <summary>
        /// First Name of the person
        /// </summary>
        /// <returns></returns>
        [JsonProperty("name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name of the person
        /// </summary>
        /// <returns></returns>
        [JsonProperty("surname")]
        public string LastName { get; set; }

        /// <summary>
        /// The sex of the person
        /// </summary>
        /// <returns></returns>
        [JsonProperty("gender")]
        public string Sex { get; set; }

        /// <summary>
        /// The region or country of the person
        /// </summary>
        /// <returns></returns>
        [JsonProperty("region")]
        public string Region { get; set; }
    }
}