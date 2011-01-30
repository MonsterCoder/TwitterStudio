using System.ComponentModel.Composition;

namespace Company.TwitterStudio
{
    [Export(typeof(ITwitterCmdHandler))]
    public class TwitterCmdHandler : ITwitterCmdHandler
    {
        public bool Twitte(string selectedText)
        {
            var vm = new TwitterPanelViewModel() { Message = "Message here", Code = selectedText };
            var win = new TwitterPanel() { DataContext = vm };
            win.ShowDialog();
            return true;
        }
    }
}