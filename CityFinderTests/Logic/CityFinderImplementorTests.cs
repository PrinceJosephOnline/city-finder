using Microsoft.VisualStudio.TestTools.UnitTesting;
using CityFinder.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityFinder.Objects;

namespace CityFinder.Logic.Tests
{
    [TestClass()]
    public class CityFinderImplementorTests
    {
        [TestMethod()]
        public void TestValidAbbreviation()
        {
            var errorMessage = string.Empty;
            var stateInput = "AZ";
            var expectedType = typeof(StateInfo);
            var state = CityFinderImplementor.Instance().GetStateDetails(stateInput, out errorMessage);

            Assert.IsNull(errorMessage, errorMessage);
            Assert.IsInstanceOfType(state, expectedType);
        }

        [TestMethod()]
        public void TestValidStateName()
        {
            var errorMessage = string.Empty;
            var stateInput = "Northern Mariana Islands";
            var expectedType = typeof(StateInfo);
            var state = CityFinderImplementor.Instance().GetStateDetails(stateInput, out errorMessage);

            Assert.IsNull(errorMessage, errorMessage);
            Assert.IsInstanceOfType(state, expectedType);
        }

        [TestMethod()]
        public void TestValidStateNameSpecial()
        {
            var errorMessage = string.Empty;
            var stateInput = "U.S. Virgin Islands";
            var expectedType = typeof(StateInfo);
            var state = CityFinderImplementor.Instance().GetStateDetails(stateInput, out errorMessage);

            Assert.IsNull(errorMessage, errorMessage);
            Assert.IsInstanceOfType(state, expectedType);
        }

        [TestMethod()]
        public void TestInvalidAbbreviation()
        {
            var errorMessage = string.Empty;
            var stateInput = "AA";
            var state = CityFinderImplementor.Instance().GetStateDetails(stateInput, out errorMessage);

            Assert.IsNotNull(errorMessage, errorMessage);
        }

        [TestMethod()]
        public void TestInvalidInput()
        {
            var errorMessage = string.Empty;
            var stateInput = "00";
            var state = CityFinderImplementor.Instance().GetStateDetails(stateInput, out errorMessage);

            Assert.IsNotNull(errorMessage, errorMessage);
        }

        [TestMethod()]
        public void TestNullInput()
        {
            var errorMessage = string.Empty;
            var stateInput = "";
            var state = CityFinderImplementor.Instance().GetStateDetails(stateInput, out errorMessage);

            Assert.IsNotNull(errorMessage, errorMessage);
        }
    }
}