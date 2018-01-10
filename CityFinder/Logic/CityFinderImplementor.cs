using CityFinder.Common;
using CityFinder.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CityFinder.Logic
{
    /// <summary>
    /// This class implements the City finder logic
    /// </summary>
    public class CityFinderImplementor
    {
        #region Member Variables

        /// <summary>
        /// This variable represents the states collection cache object
        /// </summary>
        private List<StateInfo> statesCache;

        /// <summary>
        /// This variable represents the CityFinderImplementor singleton object
        /// </summary>
        private static CityFinderImplementor cityFinder;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance of the class, singleton support
        /// </summary>
        private CityFinderImplementor()
        {
            // Do nothing, keep it private for singleton
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method creates a singleton instance of CityFinderImplementor
        /// </summary>
        /// <returns>Instance of CityFinderImplementor</returns>
        public static CityFinderImplementor Instance()
        {
            if (cityFinder == null)
            {
                cityFinder = new CityFinderImplementor();
                cityFinder.LoadStatesData();
            }

            return cityFinder;
        }

        /// <summary>
        /// This method find the details of states corresponding to the input
        /// </summary>
        /// <param name="input">User input for state</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>State if a match is found, null otherwise</returns>
        public StateInfo GetStateDetails(string input, out string errorMessage)
        {
            errorMessage = null;
            StateInfo result = null;
            try
            {
                input = input.Trim();
                LoadStatesData();
                if (statesCache != null)
                {
                    var isValid = ValidateInput(input, out errorMessage);
                    if (isValid)
                    {
                        if (input.Length == 2)
                        {
                            result = statesCache.FirstOrDefault(s => s.Abbreviation == input);
                        }
                        else
                        {
                            result = statesCache.FirstOrDefault(s => s.StateName == input);
                        }
                        if (result == null) { errorMessage = "No matching 'State' found, please try again"; }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error during the process. Please try again later. Reason: " + ex.Message);
            }

            return result;

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This method loads the data from the Remote service
        /// </summary>
        /// <param name="clearCache">Pass to true force reload data</param>
        private void LoadStatesData(bool clearCache = false)
        {
            // Load data if its not loaded already, or forced to load
            if (clearCache || statesCache == null || statesCache.Count == 0)
            {
                var errorMessage = string.Empty;
                using (var client = new WebServiceClient(Country.USA))
                {
                    statesCache = client.GetStatesFromService(out errorMessage);
                }
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    throw new Exception("Error while getting data from service.");
                    // TODO: Log the actual exceptionk
                }
            }
        }

        /// <summary>
        /// This method validates the user input
        /// </summary>
        /// <param name="input">User input for state</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns>Returns true if input is valid</returns>
        private bool ValidateInput(string input, out string errorMessage)
        {
            errorMessage = null;
            var isValid = false;

            if (!Regex.IsMatch(input, @"^[a-zA-Z\s\.]{2,50}$"))
            {
                errorMessage = "Input is not valid";
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        #endregion
    }
}
