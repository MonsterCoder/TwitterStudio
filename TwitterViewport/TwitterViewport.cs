using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Editor;

namespace TwitterViewport
{
    /// <summary>
    /// Adornment class that draws a square box in the top right hand corner of the viewport
    /// </summary>
    class TwitterViewport
    {
        private IWpfTextView _view;
        private IAdornmentLayer _adornmentLayer;

        /// <summary>
        /// Creates a square image and attaches an event handler to the layout changed event that
        /// adds the the square in the upper right-hand corner of the TextView via the adornment layer
        /// </summary>
        /// <param name="view">The <see cref="IWpfTextView"/> upon which the adornment will be drawn</param>
        public TwitterViewport(IWpfTextView view)
        {
            _view = view;

            _adornmentLayer = view.GetAdornmentLayer("TwitterViewport");

            onSizeChange();

            // Attach to the view events
            //_view.LayoutChanged += (a, e) => onSizeChange();
            _view.TextBuffer.Changed += (a, e) => onSizeChange();
            _view.ViewportHeightChanged += (a, e) => onSizeChange();
        }

        public void onSizeChange()
        {
            ////clear the adornment layer of previous adornments
            //_adornmentLayer.RemoveAllAdornments();
            //if (_view.TextViewLines == null) return;
            //foreach (var line in _view.TextViewLines)
            //{
            //    if (line.Snapshot.GetText().Contains("twitted"))
            //    {
            //        var top = line.Top;
           
            //        var panel = new TwitterPanel() { Width = 5, Height = 5 };

            //        Canvas.SetTop(panel, top);

            //        _adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, this, panel, null);

            //    }
            //}

        }
    }
}
