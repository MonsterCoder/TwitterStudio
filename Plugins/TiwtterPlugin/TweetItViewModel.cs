namespace TwitterPlugin
{
    /// <summary>
    /// View model for the tweet it window
    /// </summary>
    public class TweetItViewModel
    {
        /// <summary>
        /// Gets or sets the user input message
        /// </summary>
        public string MessageBody { get; set; }

        /// <summary>
        /// Gets or sets the current logged in user name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets if use another twitter account
        /// </summary>
        public bool UseAnotherAccount { get; set; }        
        
        /// <summary>
        /// Gets or sets if show switch account option
        /// </summary>
        public bool ShowSwith { get; set; }        
        
        /// <summary>
        /// Gets or sets if show switch account option
        /// </summary>
        public bool logTweet { get; set; }
    }
}