using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Net;
using System.Windows;
using Hammock;
using Hammock.Authentication.OAuth;
using Hammock.Serialization;
using Twitterizer;
using TwitterStudio.Domain;


namespace TiwtterStudio.Imp
{
    [Export(typeof(ICmdHandler))]
    public class TwitterHanlder : ICmdHandler
    {
        public bool Send(string msg)
        {
            var ConsumerKey = "CuiCpk9LFdVbv9f54WVUsQ";
            var ConsumerSecret = "hECzA4iDaowztNba9vPd52F1k3Z4efjfAlshPyBaqgc";
            var pin = "2042158";
            var accesskey = "JgluvAGHQ1R7CLziVvwc2Us9weqo25fr9ttrpdN3eh8";

            //var response = OAuthUtility.GetRequestToken(ConsumerKey, ConsumerSecret, "oob");
            //Debug.WriteLine(response.Token);

            var response = OAuthUtility.GetAccessToken(ConsumerKey, ConsumerSecret, accesskey, pin);  

            OAuthTokens tokens = new OAuthTokens();
            tokens.AccessToken = response.Token;
            tokens.AccessTokenSecret = response.TokenSecret;
            tokens.ConsumerKey = "CuiCpk9LFdVbv9f54WVUsQ";
            tokens.ConsumerSecret = "hECzA4iDaowztNba9vPd52F1k3Z4efjfAlshPyBaqgc";

            TwitterResponse<TwitterStatus> tweetResponse = TwitterStatus.Update(tokens, msg);
            if (tweetResponse.Result == RequestResult.Success)
            {
                MessageBox.Show("ok");
            }
            else
            {
                // Something bad happened
            }

            return true;
        }
    }
}
