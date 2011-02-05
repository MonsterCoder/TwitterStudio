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
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using TwitterStudio.Domain;

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
        [Import(typeof(ICmdHandler))]
        private ICmdHandler twitterCmdhandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterStudioPackage"/> class. 
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public TwitterStudioPackage()
        {
            InitializeImports();
        }

        /// <summary>
        /// Imports MEF components
        /// </summary>
        private void InitializeImports()
        {
            var container = new CompositionContainer(new DirectoryCatalog(@"c:\Dev\TwitterStudio\libs"));

            // Fill the imports of this object
            try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Debug.WriteLine(compositionException.ToString());
            }
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
            // Get the instance number 0 of this tool window. This window is single instance so this instance
            // is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            var window = FindToolWindow(typeof(MyToolWindow), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException(Resources.CanNotCreateWindow);
            }

            var windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
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
            var txtMgr = (IVsTextManager)GetService(typeof(SVsTextManager));
            IVsTextView view;
            IVsTextLines buffer;
            txtMgr.GetActiveView(1, null, out view);
            view.GetBuffer(out buffer);

            string selectedText;
            view.GetSelectedText(out selectedText);

            if (twitterCmdhandler.Update(selectedText))
            {
                object point;
                buffer.CreateEditPoint(0, 0, out point);
                ((EditPoint)point).Insert("/// twitter \n");
            }
        }
    }
}
