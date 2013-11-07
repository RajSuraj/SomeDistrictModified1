using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SomeDistrictModified1
{
    [Binding]
    public class SomeDistrictModified1Steps
    {
        private string _theUrl;
        private string _theResponse;
        private string _theapiresponse;

        [Given(@"the alteryx service is running at ""(.*)""")]
        public void GivenTheAlteryxServiceIsRunningAt(string p0)
        {
            _theUrl = p0;
        }
        
        [When(@"I invoke GET at ""(.*)"" for ""(.*)""")]
        public void WhenIInvokeGETAtFor(string p0, string expecteddistrict)
        {
            string fullUrl = _theUrl + "/" + p0;
            WebRequest request = WebRequest.Create(fullUrl);
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            _theResponse = responseFromServer;

            var DistrictObj = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<dynamic>(_theResponse);
            int count = DistrictObj.Length;

            int i = 0;
            for (i = 0; i <= count - 1; i++)
            {
                if (DistrictObj[i]["title"] == expecteddistrict)
                {
                    break;
                }
            }

            string id = DistrictObj[i]["id"];
            string apiUrl = fullUrl + "/" + id;
           
            WebRequest apirequest = WebRequest.Create(apiUrl);
            apirequest.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse apiresponse = (HttpWebResponse)apirequest.GetResponse();
            Stream apidataStream = apiresponse.GetResponseStream();
            StreamReader apireader = new StreamReader(apidataStream);
            string apiresponseFromServer = apireader.ReadToEnd();
            _theapiresponse = apiresponseFromServer;

        }
        
        [Then(@"I see that district description contains the text ""(.*)""")]
        public void ThenISeeThatDistrictDescriptionContainsTheText(string expecteddescription)
        {
            var dict = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, dynamic>>(_theapiresponse);

            Console.Write(dict["description"]);
            StringAssert.Contains(expecteddescription, dict["description"]);

        }
    }
}
