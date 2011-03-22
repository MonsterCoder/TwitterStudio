using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Company.TwitterStudio.Services;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;

namespace Company.TwitterStudio
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true)]

    // This attribute is used to register the informations needed to show the this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]

    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]

    // This attribute registers a tool window exposed by this package.
    [ProvideToolWindow(typeof(MyToolWindow))]
    [Guid(GuidList.guidTwitterStudioPkgString)]
    [ProvideOptionPage(typeof(OptionPage), "Twitter Studio", "Twitter Studio", 0, 0, true)]
    public sealed class TwitterStudioPackage : Package
    {
        /// <summary>
        /// Twitter command handler
        /// </summary>
        [Import(typeof(ITwitterService))]
        private ITwitterService twitterService;       
        
        /// <summary>
        /// Twitter command handler
        /// </summary>
        [Import(typeof(IPasteService))]
        private IPasteService pasteService;

        /// <summary>
        /// output window
        /// </summary>
        private IVsOutputWindowPane _outputWindow;

        /// <summary>
        /// Backfield for option page property
        /// </summary>
        private OptionPage _optionPage;

        /// <summary>
        /// Gets OutputPane.
        /// </summary>
        private IVsOutputWindowPane OutputPane
        {
            get
            {
                if (_outputWindow == null)
                {
                    var outputWindow = GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;
                    var debugPaneGuid = VSConstants.GUID_OutWindowDebugPane;
                    if (outputWindow != null)
                    {
                        outputWindow.GetPane(ref debugPaneGuid, out _outputWindow);
                    }
                }

                return _outputWindow;
            }
        }

        /// <summary>
        /// Gets the option page
        /// </summary>
        private OptionPage OptionsPage
        {
            get
            {
                if (_optionPage == null)
                {
                    _optionPage = (OptionPage)GetDialogPage(typeof(OptionPage));
                }

                return _optionPage;
            }
        }

        /// <summary>
        /// Imports MEF components
        /// </summary>
        private void InitializeImports()
        {
            var vscontainer = GetService(typeof(SComponentModel)) as IComponentModel;

            if (vscontainer != null)
            {
                vscontainer.DefaultCompositionService.SatisfyImportsOnce(this);
            }
        }

        /// <summary>
        /// Initialization of the package; 
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
 
            // Add our command handlers for menu (commands must exist in the .vsct file)
            var mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (null != mcs)
            {
                // Create the command for the menu item.
                var menuCommandID = new CommandID(GuidList.guidTwitterStudioCmdSet, (int) PkgCmdIDList.cmdidMyCommand);
               
                var menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
               
                mcs.AddCommand(menuItem);
            }

            InitializeImports();
        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// </summary>
        /// <param name="sender">
        /// The sender to use
        /// </param>
        /// <param name="e">
        /// The event argument
        /// </param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            if (pasteService == null || twitterService == null)
            {
                InitializeImports();
            }

            var txtMgr = (IVsTextManager)GetService(typeof(SVsTextManager));
            IVsTextView view;
            txtMgr.GetActiveView(1, null, out view);

            if (view == null)
            {
                return;
            }

            string selectedText;
            view.GetSelectedText(out selectedText);

            twitterService.MaximumMsgLength = OptionsPage.MaxLength;

            if (OptionsPage.RememberAccessPin)
            {
                twitterService.AccessPin = OptionsPage.AccessPin;
            }

            Tweet(selectedText);
             
        }

        /// <summary>
        /// </summary>
        /// <param name="selectedText">
        /// The selected text.
        /// </param>
        private void Tweet(string selectedText)
        {
            var link = pasteService.Upload(selectedText);

            twitterService.Update(link, UpdateCallBack);
        }

        /// <summary>
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        private void UpdateCallBack(string msg)
        {
            if (OptionsPage.RememberAccessPin)
            {
                OptionsPage.AccessPin = twitterService.AccessPin;
                OptionsPage.SaveSettingsToStorage();
            }

            OutputPane.Activate();
            OutputPane.OutputString(msg);
        }
    }
}
