using System;
using System.ComponentModel.Composition;
using Company.TwitterStudio.Services;
using Twitterizer;

namespace TwitterPlugin
{
    /// <summary>
    /// Class for handling twitte requests
    /// </summary>
    [Export(typeof(ITwitterSerive))]
    public class TwitterPlugin : ITwitterSerive
    {
        /// <summary>
        /// Twitter Api cunsume key
        /// </summary>
        private const string ConsumerKey = "CuiCpk9LFdVbv9f54WVUsQ";

        /// <summary>
        ///  Twitter Api cunsume secret
        /// </summary>
        private const string ConsumerSecret = "hECzA4iDaowztNba9vPd52F1k3Z4efjfAlshPyBaqgc";

        /// <summary>
        /// Access key to use
        /// </summary>
        private static string accessKey = string.Empty;

        /// <summary>
        /// Access secret to use;
        /// </summary>
        private static string accessSecret = string.Empty;

        /// <summary>
        /// Name of current logged in user
        /// </summary>
        private static string currentUser;

        /// <summary>
        /// Send a message
        /// </summary>
        /// <param name="link">
        /// The content to be sent
        /// </param>
        /// <param name="logTweet"></param>
        /// <returns>
        /// bool to indicate if the update succeed
        /// </returns>
        public bool Update(string link, Action<string> logTweet)
        {
            var vm = new TweetItViewModel()
                         {
                             ShowSwith = !string.IsNullOrEmpty(accessKey),
                             Username = currentUser
                         };

             new TweetItWindow { DataContext = vm } .ShowDialog();

            if (string.IsNullOrEmpty(accessKey) || vm.UseAnotherAccount)
            {
                InitializeAccessKey();
            }

            var tokens = new OAuthTokens
                             {
                                 AccessToken = accessKey,
                                 AccessTokenSecret = accessSecret,
                                 ConsumerKey = ConsumerKey,
                                 ConsumerSecret = ConsumerSecret
                             };
            var body = string.Format("{0}\n{1}", vm.MessageBody, link);
            var result = TwitterStatus.Update(tokens, body).Result == RequestResult.Success;
            if (vm.logTweet)
            {
                logTweet(body);
            }

            return result;
        }

        /// <summary>
        /// Initialze the access key and secrete
        /// </summary>
        private static void InitializeAccessKey()
        {
            var oAuth_token = OAuthUtility.GetRequestToken(ConsumerKey, ConsumerSecret, "oob").Token;

            // redirect the user to twitter and get the access pin
            var pin = GetPin(oAuth_token);

            var oAuthTokenResponse = OAuthUtility.GetAccessToken(ConsumerKey, ConsumerSecret, oAuth_token, pin);

            accessKey = oAuthTokenResponse.Token;
            accessSecret = oAuthTokenResponse.TokenSecret;
            currentUser = oAuthTokenResponse.ScreenName;
        }

        /// <summary>
        /// Form that enable the user to login to twitter and get the pin
        /// </summary>
        /// <param name="tempAccessKey">
        /// The temp access key.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        private static string GetPin(string tempAccessKey)
        {
            var pin = string.Empty;

            var authorizeAccess = new AccessPinWindow(tempAccessKey, rtPin => pin = rtPin);
            authorizeAccess.ShowDialog();

            if (string.IsNullOrEmpty(pin))
            {
                throw new ApplicationException("Can not get Twitter access pin!");
            }

            return pin;
        }
    }
}
