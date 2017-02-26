using Newtonsoft.Json;

namespace PeopleSearch.Models.V1
{
    /// <summary>
    /// 
    /// </summary>   
    public class NameApiResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [JsonProperty("name")]
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [JsonProperty("surname")]
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [JsonProperty("gender")]
        public string Sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [JsonProperty("region")]
        public string Region { get; set; }
    }
}