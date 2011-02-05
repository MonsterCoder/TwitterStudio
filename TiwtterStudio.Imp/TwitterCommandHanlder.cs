using System;
using System.ComponentModel.Composition;
using Twitterizer;
using TwitterStudio.Domain;


namespace TiwtterStudio.Imp
{
    /// <summary>
    /// Class for handling twitte requests
    /// </summary>
    [Export(typeof(ICmdHandler))]
    public class TwitterCommandHanlder : ICmdHandler
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
        /// Send a message
        /// </summary>
        /// <param name="msg">
        /// The content to be sent
        /// </param>
        /// <returns>
        /// bool to indicate if the update succeed
        /// </returns>
        public bool Update(string msg)
        {
            if (string.IsNullOrEmpty(accessKey))
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

            return TwitterStatus.Update(tokens, msg).Result == RequestResult.Success;
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

            var login = new login(tempAccessKey, rtPin => pin = rtPin);
            login.ShowDialog();

            if (string.IsNullOrEmpty(pin))
            {
                throw new ApplicationException("Failed in getting Twitter access pin!");
            }

            return pin;
        }
    }
}
