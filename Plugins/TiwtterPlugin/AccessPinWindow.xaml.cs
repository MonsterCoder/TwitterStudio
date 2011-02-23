using System;
using System.Windows;
using System.Windows.Forms;

namespace TwitterPlugin
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AccessPinWindow
    {
        private readonly Action<string> _returnPin;

        public AccessPinWindow(string accessKey, Action<string> returnPin)
        {
            _returnPin = returnPin;
            InitializeComponent();
            this.webBrowser1.Navigate(string.Format( "http://twitter.com/oauth/authorize?oauth_token={0}",accessKey));
            webBrowser1.DocumentCompleted += processDocument;
        }

        private void processDocument(object sender, WebBrowserDocumentCompletedEventArgs webBrowserDocumentCompletedEventArgs)
        {
            if (webBrowser1.Document != null)
            {
                var element = webBrowser1.Document.GetElementById("oauth_pin");

                if (element != null)
                {
                    _returnPin(element.InnerText.Trim());
                    this.Close();
                }
            }
        }
    }
}
