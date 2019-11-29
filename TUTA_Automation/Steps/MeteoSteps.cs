using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace TUTA_Automation.Steps
{
    [Binding]
    public class MeteoSteps
    {

        public static HttpClient HttpClient { get; private set; }
        public static HttpRequestMessage HttpRequestMessage { get; private set; }
        public static HttpResponseMessage HttpResponseMessage { get; private set; }

        internal static MeteoObject MeteoReults { get => meteoResult; set => meteoResult = value; }

        private static MeteoObject meteoResult = new MeteoObject() { };

        private static string _cityUri;

        public static void SwapOutHttpClient(HttpClient client)
        {
            HttpClient = client;
        }

        [BeforeScenario]
        public void Before()
        {
            if (HttpClient == null)
                HttpClient = new HttpClient();

            if (HttpRequestMessage == null)
                HttpRequestMessage = new HttpRequestMessage();
        }

        [Given(@"I am using meteo base url '(.*)' value")]
        public void GivenIAmUsingMeteoBaseUrlHttpApi(string baseUrl)
        {
            if (HttpClient.BaseAddress == null)
            {
                HttpClient.BaseAddress = new Uri(baseUrl);
            }
        }

        [Given(@"I setup the request to GET for city resource '(.*)' value")]
        public void GivenISetupTheRequestToGETForCityResourceValue(string cityValue)
        {
            HttpRequestMessage = new HttpRequestMessage();
            HttpRequestMessage.Method = new HttpMethod("GET");
            _cityUri = cityValue;
        }

        [When(@"I send the meteo request")]
        public void WhenISendTheMeteoRequest()
        {
            HttpRequestMessage.RequestUri = new Uri(_cityUri, UriKind.Relative);
            HttpResponseMessage = HttpClient.SendAsync(HttpRequestMessage).Result;
        }

        [Then(@"I should receive meteo response")]
        public void ThenIShouldReceiveMeteResponse()
        {
            string json = @"" + HttpResponseMessage.Content.ReadAsStringAsync().Result + "";

            meteoResult = JsonConvert.DeserializeObject<MeteoObject>(json);
        }

        [Then(@"I should have response status code of (.*)")]
        public void ThenIShouldHaveResponseStatusCodeOf(int responseCode)
        {
    
            Assert.That((int)HttpResponseMessage.StatusCode, Is.EqualTo(responseCode));
        }

        [Then(@"I validate condition Code content should have '(.*)' value")]
        public void ThenIValidateContentShouldHaveValue(string condition)

        {
            Assert.That(meteoResult.forecastTimestamp.conditionCode, Is.Not.Empty,
            "Something went wrong!\n" +
            "Expected result is: "+ condition +"\n" +
            "Actual result is: '" + meteoResult.forecastTimestamp.conditionCode + "'.");
        }

        [Then(@"I validate place code content should have '(.*)' value")]
        public void ThenIValidateCodeValue(string codeval)

        {
            Assert.That(meteoResult.place.country, Is.Not.Empty,
            "Something went wrong!\n" +
            "Expected result is: " + codeval + "\n" +
            "Actual result is: '" + meteoResult.place.country + "'.");
        }

        [Then(@"I validate place country content should have '(.*)' value")]
        public void ThenIValidateCountryValue(string countryval)

        {
            Assert.That(meteoResult.place.country, Is.Empty,
            "Something went wrong!\n" +
            "Expected result is: "+countryval+"\n" +
            "Actual result is: '" + meteoResult.place.country + "'.");
        }

        [Then(@"I validate place name should have (.*) value")]
        public void ThenIValidatePlaceNameShouldHaveValue(string name)
        {
            Assert.That(meteoResult.place.name == name,
            "Something went wrong!\n" + 
           "Expected result is: " + name + "\n" +
            "Actual result is: '" + meteoResult.place.name + "'.");
        }
    }
}

