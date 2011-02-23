using System.Windows;

namespace TwitterPlugin
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class TweetItWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TweetItWindow"/> class.
        /// </summary>
        public TweetItWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
