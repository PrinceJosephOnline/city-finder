using CityFinder.Common;
using CityFinder.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;

namespace CityFinder.Logic
{
    /// <summary>
    /// This class implements the api client
    /// </summary>
    internal class WebServiceClient : IDisposable
    {
        #region Member Variables

        private HttpClient client;

        private string serviceUrl;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance of the WebServiceClient, sets country
        /// </summary>
        /// <param name="country"></param>
        public WebServiceClient(Country country)
        {
            switch (country)
            {
                default:
                    serviceUrl = ConfigurationManager.AppSettings["CountryServiceUSA"];
                    break;
            }
            if (string.IsNullOrEmpty(serviceUrl))
            {
                throw new Exception("Service configuration is missing, please contact IT support");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method implments the IDisposable dispose method
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (client != null)
                {
                    client.Dispose();
                    client = null;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log excepton here
            }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// This method gets the states data from service
        /// </summary>
        /// <param name="errorMessage">Error message if any</param>
        /// <returns>List of states if found, null otherwise</returns>
        internal List<StateInfo> GetStatesFromService(out string errorMessage)
        {
            errorMessage = null;
            List<StateInfo> states = null;
            try
            {
                client = new HttpClient();
                var response = client.GetAsync(serviceUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<RestResponseWrapper>(data);
                    if (result == null || result.RestResponseData.States == null || result.RestResponseData.States.Count == 0)
                    {
                        errorMessage = result.RestResponseData?.Messages?.FirstOrDefault();
                    }
                    else
                    {
                        states = result.RestResponseData.States;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                // TODO: Log exception here
            }

            return states;
        }

        #endregion
    }
}