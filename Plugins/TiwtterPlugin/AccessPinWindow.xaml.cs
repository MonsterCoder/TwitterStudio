using System;
using System.Windows.Navigation;
using HtmlAgilityPack;

namespace TwitterPlugin
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AccessPinWindow
    {
        /// <summary>
        /// action for returning pin
        /// </summary>
        private readonly Action<string> returnPin;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessPinWindow"/> class.
        /// </summary>
        /// <param name="accessKey">
        /// The access key.
        /// </param>
        /// <param name="returnPin">
        /// The return pin.
        /// </param>
        public AccessPinWindow(string accessKey, Action<string> returnPin)
        {
            this.returnPin = returnPin;
            InitializeComponent();
            this.webBrowser1.Navigate(string.Format("http://twitter.com/oauth/authorize?oauth_token={0}", accessKey));
            webBrowser1.LoadCompleted += ExtractPin;
        }

        /// <summary>
        /// Extracts Twitter access pin
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="navigationEventArgs">
        /// The navigation event args.
        /// </param>
        private void ExtractPin(object sender, NavigationEventArgs navigationEventArgs)
        {
            dynamic doc = webBrowser1.Document;

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(doc.body.innerHTML);

            var element = htmlDocument.GetElementbyId("oauth_pin");

            if (element != null)
            {
                returnPin(element.InnerText.Trim());
                this.Close();
            }
        }
    }
}
