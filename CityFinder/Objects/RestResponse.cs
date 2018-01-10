using Newtonsoft.Json;
using System.Collections.Generic;

namespace CityFinder.Objects
{
    /// <summary>
    /// This class represents the API response data
    /// </summary>
    public class RestResponse
    {
        #region Properties

        [JsonProperty("messages")]
        public List<string> Messages { get; set; }

        [JsonProperty("result")]
        public List<StateInfo> States { get; set; }

        #endregion
    }
}
