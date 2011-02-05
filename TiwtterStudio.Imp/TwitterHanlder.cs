using System.ComponentModel.Composition;
using System.Net;
using System.Windows;
using Hammock;
using Hammock.Authentication.OAuth;
using Hammock.Serialization;
using PostmarkDotNet;
using TwitterStudio.Domain;


namespace TiwtterStudio.Imp
{
    [Export(typeof(ICmdHandler))]
    public class TwitterHanlder : ICmdHandler
    {
        public bool Send(string msg)
        {

            var credentials = new OAuthCredentials
            {
                Type = OAuthType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                ConsumerKey = "CuiCpk9LFdVbv9f54WVUsQ",
                ConsumerSecret = "hECzA4iDaowztNba9vPd52F1k3Z4efjfAlshPyBaqgc",
            };



            var serializer = new HammockDataContractJsonSerializer();

            //var message = new PostmarkMessage
            //{
            //    From = "Codecamp2011",
            //    To = "VSX demo",
            //    Subject = "Tweet code",
            //    TextBody = msg
            //};

            var client = new RestClient
            {
                Credentials = credentials,
                Authority = "http://twitter.com/oauth",
                Serializer = serializer,
                Deserializer = serializer
            };


            client.AddHeader("Accept", "application/json");
            client.AddHeader("Expect", " ");
            client.AddHeader("Content-Type", "application/json; charset=utf-8");
            client.AddHeader("X-Postmark-Server-Token", "ServerToken");
            client.AddHeader("User-Agent", "Hammock");

            var request = new RestRequest
            {

                Path = "request_token",
                Entity = msg
            };
            System.Net.ServicePointManager.Expect100Continue = false;
            var response = client.Request(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("OK");
            }


            return true;
        }
    }
}
