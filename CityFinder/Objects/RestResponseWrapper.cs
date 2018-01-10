using Newtonsoft.Json;

namespace CityFinder.Objects
{
    /// <summary>
    /// This class represents the API response root data
    /// </summary>
    public class RestResponseWrapper
    {
        #region Proeprties

        [JsonProperty("RestResponse")]
        public RestResponse RestResponseData { get; set; }

        #endregion
    }
}
