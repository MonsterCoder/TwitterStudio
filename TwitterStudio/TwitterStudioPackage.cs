// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TwitterStudioPackage.cs" company="">
//   
// </copyright>
// <summary>
//   This is the class that implements the package exposed by this assembly.
//   The minimum requirement for a class to be considered a valid package for Visual Studio
//   is to implement the IVsPackage interface and register itself with the shell.
//   This package uses the helper classes defined inside the Managed Package Framework (MPF)
//   to do it: it derives from the Package class that provides the implementation of the
//   IVsPackage interface and uses the registration attributes defined in the framework to
//   register itself and its components with the shell.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Company.TwitterStudio.Services;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;

namespace Company.TwitterStudio
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell.
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
    public sealed class TwitterStudioPackage : Package
    {
        /// <summary>
        /// Twitter command handler
        /// </summary>
        [Import(typeof(ITwitterSerive))]
        private ITwitterSerive twitterService;       
        
        /// <summary>
        /// Twitter command handler
        /// </summary>
        [Import(typeof(IPasteService))]
        private IPasteService pasteService;

        /// <summary>
        /// Imports MEF components
        /// </summary>
        private void InitializeImports()
        {
            var vscontainer = GetService(typeof(SComponentModel)) as IComponentModel;
            
            vscontainer.DefaultCompositionService.SatisfyImportsOnce(this);
        }

        /// <summary>
        /// This function is called when the user clicks the menu item that shows the 
        /// tool window. See the Initialize method to see how the menu item is associated to 
        /// this function using the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        /// <param name="sender">
        /// The sender to use
        /// </param>
        /// <param name="e">
        /// The event argument
        /// </param>
        private void ShowToolWindow(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initilaization code that rely on services provided by VisualStudio.
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

                var toolwndCommandID = new CommandID(GuidList.guidTwitterStudioCmdSet, (int)PkgCmdIDList.cmdidMyTool);
                var menuToolWin = new MenuCommand(ShowToolWindow, toolwndCommandID);
                mcs.AddCommand(menuToolWin);
            }
        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
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
            IVsTextLines buffer;
            txtMgr.GetActiveView(1, null, out view);
            if (view == null)
            {
                return;
            }

            view.GetBuffer(out buffer);

            string selectedText;
            view.GetSelectedText(out selectedText);
            
            var link = pasteService.Upload(selectedText);

            if (twitterService.Update(link))
            {
                object point;
                int l1;
                int c1;
                int l2;
                int c2;
                view.GetSelection(out l1, out c1, out l2, out c2);
                buffer.CreateEditPoint(Math.Max(l1, l2), 0, out point);

                ((EditPoint)point).Insert(string.Format("/// twitted:{0}", link));
            }
            else
            {
                MessageBox.Show(Resources.TwitterStudioPackage_MenuItemCallback_Tweet_failed_);
            }
        }
    }
}
