using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace Company.TwitterStudio
{
    /// <summary>
    /// Option page
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    public class OptionPage : DialogPage
    {
        /// <summary>
        /// </summary>
        private int _maxLength = 10;

        /// <summary>
        /// </summary>
        private bool _shorten = true;

        /// <summary>
        /// </summary>
        private bool _rememberAccessKey = true;

        /// <summary>
        /// Gets or sets MaxLength.
        /// </summary>
        [Category("Twitter"), DisplayName(@"Maxium message Lenght"), Description("Max lenght of message you can input to the message window")]
        public int MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }


        /// <summary>
        /// Gets or sets a value indicating whether Shorten.
        /// </summary>
        [Category("Pastbin"), DisplayName(@"Use Shortener"), Description("User url shortener")]
        public bool Shorten
        {
            get { return _shorten; }
            set { _shorten = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether rember AccessKey.
        /// </summary>
        [Category("Twitter"), DisplayName(@"Remmber Access Key"), Description("Remember Twitter app access key")]
        public bool RememberAccessKey
        {
            get { return _rememberAccessKey; }
            set { _rememberAccessKey = value; }
        }

        /// <summary>
        /// Gets or sets the access key
        /// </summary>
        [Category("Twitter"), DisplayName(@"Access Key"), Description("Twitter app access key"), ReadOnly(true)]        
        public string AccessKey { get; set; }

    }
}