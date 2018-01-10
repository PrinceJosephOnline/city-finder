using Newtonsoft.Json;

namespace CityFinder.Objects
{
    /// <summary>
    /// This class represents the API response root data
    /// </summary>
    public class StateInfo
    {
        #region Properties

        [JsonProperty("name")]
        public string StateName { get; set; }

        [JsonProperty("abbr")]
        public string Abbreviation { get; set; }

        [JsonProperty("largest_city")]
        public string LargestCity { get; set; }

        [JsonProperty("capital")]
        public string Capital { get; set; }

        #endregion
    }
}
