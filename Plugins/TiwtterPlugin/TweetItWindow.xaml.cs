using System.Windows;

namespace TwitterPlugin
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class TweetItWindow : Window
    {
        private Thickness _thickness_1 = new Thickness(1);
        private Thickness _thickness_0 = new Thickness(0);

        /// <summary>
        /// Initializes a new instance of the <see cref="TweetItWindow"/> class.
        /// </summary>
        public TweetItWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void MessageBody_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            this.Warningbox.BorderThickness = this.MessageBody.MaxLength <= this.MessageBody.Text.Length ? _thickness_1 : _thickness_0;
        }
    }
}
