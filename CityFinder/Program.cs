using CityFinder.Logic;
using System;

namespace CityFinder
{
    /// <summary>
    /// This class implments the City Finder porgram
    /// </summary>
    class Program
    {
        #region Member Variables

        /// <summary>
        /// This variable represents the display format
        /// </summary>
        private static string resultFormat = "State: {0} ({1}), Largest City: {2}, Capital: {3}";

        #endregion

        #region Entry Point

        static void Main(string[] args)
        {
            string errorMessage = null;
            while (true)
            {
                try
                {
                    Console.Write("Enter a State name or abbreviation: ");
                    var input = Console.ReadLine();
                    input = input.Trim();
                    var state = CityFinderImplementor.Instance().GetStateDetails(input, out errorMessage);
                    if (state != null)
                    {
                        Console.WriteLine(string.Format(resultFormat, state.StateName, state.Abbreviation, state.LargestCity, state.Capital));
                    }
                    else
                    {
                        Console.WriteLine(errorMessage);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Somethingwent wrong, please try again. (Reason: " + ex.Message + ")");
                }
                Console.WriteLine("");
            }
        }

        #endregion
    }
}