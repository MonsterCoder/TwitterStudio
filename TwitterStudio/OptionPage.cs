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
        private bool _rememberAccessPin = true;

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
        [Category("Twitter"), DisplayName(@"Remember Access Pin"), Description("Remember Twitter user access Pin")]
        public bool RememberAccessPin
        {
            get
            {
                return _rememberAccessPin;
            }

            set
            {
                _rememberAccessPin = value;
                if (! _rememberAccessPin)
                {
                    AccessPin = string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets the access key
        /// </summary>
        [Category("Twitter"), DisplayName(@"Access Pin"), Description("Twitter app access Pin")]        
        public string AccessPin { get; set; }

    }
}