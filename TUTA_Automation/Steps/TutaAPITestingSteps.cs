using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net.Http;
using TechTalk.SpecFlow;
using TUTA_Automation.Entities.JsonObjects;

namespace TUTA_Automation.Steps
{
    [Binding]
    public class TutaAPITestingSteps
    {
        public static HttpClient HttpClient { get; private set; }
        public static HttpRequestMessage HttpRequestMessage { get; private set; }
        public static HttpResponseMessage HttpResponseMessage { get; private set; }
        internal static PostCodeObject PostCodeResult { get => postCodeResult; set => postCodeResult = value; }

        private static PostCodeObject postCodeResult = new PostCodeObject() { };

        private static string _postCodeUri;

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

        [Given(@"I am using the base url '(.*)' value")]
        public void GivenIAmUsingTheBaseUrlHttpApi(string baseUrl)
        {
            if (HttpClient.BaseAddress == null)
            {
                HttpClient.BaseAddress = new Uri(baseUrl);
            }
        }

        [Given(@"I setup the request to GET for resource 'LS3 1EP' value")]
        public void GivenISetupTheRequestToGETForResourceLS31EP()
        {
            HttpRequestMessage = new HttpRequestMessage();
            HttpRequestMessage.Method = new HttpMethod("GET");
            _postCodeUri = "LS3 1EP";
        }

        [When(@"I send the request")]
        public void WhenISendTheRequest()
        {
            HttpRequestMessage.RequestUri = new Uri(_postCodeUri, UriKind.Relative);
            HttpResponseMessage = HttpClient.SendAsync(HttpRequestMessage).Result;
        }

        [Then(@"I should receive a response")]
        public void ThenIShouldReceiveAResponse()
        {
            string json = @"" + HttpResponseMessage.Content.ReadAsStringAsync().Result + "";

            postCodeResult = JsonConvert.DeserializeObject<PostCodeObject>(json);
        }


        [Then(@"I should have a status code of 200")]
        public void ThenIShouldHaveAStatusCodeOf200()
        {
            ScenarioContext.Current.Pending();
        }
               
        [Then(@"I validate status should have 404 value")]
        public void ThenIValidateStatusShouldHaveValue()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I validate admind_district should have 'Leeds' value")]
        public void ThenIValidateAdmind_DistrictShouldHaveValue()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
