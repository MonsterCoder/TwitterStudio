using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using TwitterStudio.Domain;

namespace TiwtterStudio.Imp
{
    [Export(typeof(ICmdHandler))]
    public class TwitterHanlder : ICmdHandler
    {
        public bool Send(string msg)
        {
            MessageBox.Show(msg);
            return true;
        }
    }
}
