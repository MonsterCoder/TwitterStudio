using System;
using System.ComponentModel.Composition;
using Company.TwitterStudio.Services;
using Twitterizer;

namespace TwitterPlugin
{
    /// <summary>
    /// Class for handling twitte requests
    /// </summary>
    [Export(typeof(ITwitterService))]
    public class TwitterPlugin : ITwitterService
    {
        /// <summary>
        /// Twitter Api cunsume key
        /// </summary>
        private const string ConsumerKey = "71GklI3cT7bTnLIKon50Rg";

        /// <summary>
        ///  Twitter Api cunsume secret
        /// </summary>
        private const string ConsumerSecret = "sBx3M7wKUhOkSTfWvDlPWi0rcoSZH2gUk3NjvoAy5w";

        /// <summary>
        /// Access secret to use;
        /// </summary>
        private static string _accessSecret = string.Empty;

        /// <summary>
        /// </summary>
        private static string _accessToken = string.Empty;

        /// <summary>
        /// Name of current logged in user
        /// </summary>
        private static string currentUser = "Not login yet";


        /// <summary>
        /// Gets or sets the access Pin
        /// </summary>
        public string AccessPin { get; set; }

        /// <summary>
        /// Gets or sets the maximum msg length
        /// </summary>
        public int MaximumMsgLength { get; set; }

        /// <summary>
        /// Updates a message
        /// </summary>
        /// <param name="link">link to code past</param>
        /// <param name="logTweet">logging method</param>
        /// <returns>if the update success</returns>
        public bool Update(string link, Action<string> logTweet)
        {
            var vm = new TweetItViewModel()
                         {
                             ShowSwith = !string.IsNullOrEmpty(AccessPin),
                             Username = currentUser
                         };

            var tweetItWindow = new TweetItWindow
                                    {
                                        DataContext = vm, 
                                        MessageBody = { MaxLength = MaximumMsgLength }
                                    };

            if (tweetItWindow.ShowDialog() != true)
            {
                return true;
            }


            InitializeAccessKey(vm.UseAnotherAccount);

            var tokens = new OAuthTokens
                             {
                                 AccessToken = _accessToken,
                                 AccessTokenSecret = _accessSecret,
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
        private void InitializeAccessKey(bool useAnotherAccount)
        {
            if (!string.IsNullOrEmpty(_accessToken) && !string.IsNullOrEmpty(_accessSecret) && !useAnotherAccount)
            {
                return;
            }

            var oAuth_token = OAuthUtility.GetRequestToken(ConsumerKey, ConsumerSecret, "oob").Token;

            // redirect the user to twitter and get the access pin
            var pin = string.IsNullOrEmpty(AccessPin) || useAnotherAccount ? GetPin(oAuth_token) : AccessPin;

            var oAuthTokenResponse = OAuthUtility.GetAccessToken(ConsumerKey, ConsumerSecret, oAuth_token, pin);

            AccessPin = pin;
            _accessToken = oAuthTokenResponse.Token;
            _accessSecret = oAuthTokenResponse.TokenSecret;
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
