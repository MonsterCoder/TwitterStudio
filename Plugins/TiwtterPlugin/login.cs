﻿using System;
using System.Windows.Forms;

namespace TwitterPlugin
{
    public partial class login : Form
    {
        private readonly Action<string> _returnPin;

        public login(string accessKey, Action<string> returnPin)
        {
            _returnPin = returnPin;
            InitializeComponent();
            this.webBrowser1.Navigate(string.Format( "http://twitter.com/oauth/authorize?oauth_token={0}",accessKey));
            webBrowser1.DocumentCompleted += processDocument;
        }

        private void processDocument(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Document.Body.InnerHtml.Contains("oauth_pin"))
            {
                _returnPin(webBrowser1.Document.GetElementById("oauth_pin").InnerText.Trim());
                this.Close();
            }
        }
    }
}
