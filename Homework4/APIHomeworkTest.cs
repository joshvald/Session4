using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Homework4
{
    [TestClass]
    public class APIHomeworkTest
    {
        // Global Variable
        private readonly ServiceReference1.CountryInfoServiceSoapTypeClient countryInfoTest =
            new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient
                .EndpointConfiguration.CountryInfoServiceSoap);

        /// <summary>
        /// Create a test method to validate the return of ‘ListOfCountryNamesByCode()’ API is by Ascending Order of Country Code
        /// </summary>
        [TestMethod]
        public void AscendingOrderOfCountryCodeTest()
        {
            // VERIFY
            var returnedResult = countryInfoTest.ListOfCountryNamesByCode();
            var expectedCountryCode = returnedResult.OrderBy(x => x.sISOCode);

            Assert.IsTrue(returnedResult.SequenceEqual(expectedCountryCode));
        }

        /// <summary>
        /// Create a test method to validate passing of invalid Country Code to ‘CountryName()’ API returns ‘Country not found in the database’
        /// </summary>
        [TestMethod]
        public void InvalidCountryCodeTest()
        {
            // Verify 
            var invalidCountryCode = "ZZ";
            var response = countryInfoTest.CountryName(invalidCountryCode);

            Assert.IsTrue(response.Contains("Country not found in the database"), "Country code found in the database");

        }

        /// <summary>
        /// Create a test method that gets the last entry from ‘ListOfCountryNamesByCode()’ API and pass the return value Country Code to
        /// ‘CountryName()’ API then validate the Country Name from both API is the same
        /// </summary>
        [TestMethod]
        public void GetsTheLastEntryTest()
        {
            // Verify 
            var lastEntry = countryInfoTest.ListOfCountryNamesByCode().Last();
            var returnedCountryName = countryInfoTest.CountryName(lastEntry.sISOCode);

            Assert.AreEqual(lastEntry.sName, returnedCountryName, "Country name mismatch");

        }
    }
}