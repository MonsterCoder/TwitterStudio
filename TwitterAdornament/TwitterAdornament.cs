using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Editor;

namespace TwitterAdornament
{
    /// <summary>
    /// Adornment class that draws a square box in the top right hand corner of the viewport
    /// </summary>
    class TwitterAdornament
    {
        private IWpfTextView _view;
        private IAdornmentLayer _adornmentLayer;

        /// <summary>
        /// Creates a square image and attaches an event handler to the layout changed event that
        /// adds the the square in the upper right-hand corner of the TextView via the adornment layer
        /// </summary>
        /// <param name="view">The <see cref="IWpfTextView"/> upon which the adornment will be drawn</param>
        public TwitterAdornament(IWpfTextView view)
        {
            _view = view;

            foreach (var line in _view.TextSnapshot.Lines)
            {
                var text = line.GetText();
                if (text.Contains("twitted:"))
                {
                  ///  var link = text.Replace("twitted:", "");

                    var panel = new TwitterPanel() { Width = 50, Height = 50 };

                  ///  Canvas.SetLeft(panel, _view.ViewportRight - line.);
                    Canvas.SetTop(panel, _view.ViewportTop + line.Start);

                    //Grab a reference to the adornment layer that this adornment should be added to
                    _adornmentLayer = view.GetAdornmentLayer("TwitterAdornament");

                    _adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, panel, null);
                }
            }

            //Brush brush = new SolidColorBrush(Colors.BlueViolet);
            //brush.Freeze();
            //Brush penBrush = new SolidColorBrush(Colors.Red);
            //penBrush.Freeze();
            //Pen pen = new Pen(penBrush, 0.5);
            //pen.Freeze();

            ////draw a square with the created brush and pen
            //System.Windows.Rect r = new System.Windows.Rect(0, 0, 30, 30);
            //Geometry g = new RectangleGeometry(r);
            //GeometryDrawing drawing = new GeometryDrawing(brush, pen, g);
            //drawing.Freeze();

            //DrawingImage drawingImage = new DrawingImage(drawing);
            //drawingImage.Freeze();

            //_image = new Image();
            //_image.Source = drawingImage;


            //_view.ViewportHeightChanged += delegate { this.onSizeChange(); };
            //_view.ViewportWidthChanged += delegate { this.onSizeChange(); };
        }

        public void onSizeChange()
        {
            //clear the adornment layer of previous adornments
            //_adornmentLayer.RemoveAllAdornments();

            //Place the image in the top right hand corner of the Viewport
            //Canvas.SetLeft(_image, _view.ViewportRight - 60);
            //Canvas.SetTop(_image, _view.ViewportTop + 30);

            //add the image to the adornment layer and make it relative to the viewport
            //_adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, _image, null);
        }
    }
}
