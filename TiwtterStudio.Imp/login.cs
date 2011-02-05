using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TiwtterStudio.Imp
{
    public partial class login : Form
    {
        public login(string accessKey)
        {
            InitializeComponent();
            this.webBrowser1.Navigate(string.Format( "http://twitter.com/oauth/authorize?oauth_token={0}",accessKey));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            var doc = this.webBrowser1.Document;
        }
    }
}
