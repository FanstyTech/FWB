using FWB.PowerTool.Model;
using FWB.PowerTool.Views;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace FWB.PowerTool
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid("90fa0204-58a6-4b51-9e71-e3d30347f8ae")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(FWB.PowerTool.Views.CreateEntity))]
    public sealed class FWBPowerToolPackage : AsyncPackage
    {
        /// <summary>
        /// FWB.PowerToolPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "90fa0204-58a6-4b51-9e71-e3d30347f8ae";

        public const int CommandId = 0x0100;
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        //protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        //{

        //    await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
        //    var commandService = await GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
        //    if (commandService != null)
        //    {
        //        var menuCommandID = new CommandID(new Guid("guidPowerToolsPackageCmdSet"), CommandId);
        //        var menuItem = new MenuCommand(this.ShowToolWindow, menuCommandID);
        //        commandService.AddCommand(menuItem);
        //    }
        //    // When initialized asynchronously, the current thread may be a background thread at this point.
        //    // Do any initialization that requires the UI thread after switching to the UI thread.
        //    await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
        //    await ToolWindow1Command.InitializeAsync(this);
        //}

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            var commandService = await GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            if (commandService != null)
            {
                // Command ID for ToolWindow1

                var createEntityCommandID = new CommandID(GuidList.guidFWBPowerToolPackageCmdSet, CommandIDs.CreateEntity);
                var createEntityMenuItem = new MenuCommand(this.ShowToolWindow, createEntityCommandID);
                commandService.AddCommand(createEntityMenuItem);

                var reGenerateEntityCommandID = new CommandID(GuidList.guidFWBPowerToolPackageCmdSet, CommandIDs.ReGenerateEntity);
                var reGenerateEntityMenuItem = new MenuCommand(this.ShowToolWindow, reGenerateEntityCommandID);
                commandService.AddCommand(reGenerateEntityMenuItem);


                var aboutFWBCommandID = new CommandID(GuidList.guidFWBPowerToolPackageCmdSet, CommandIDs.AboutFWB);
                var aboutFWBMenuItem = new MenuCommand(this.AboutFWBHandler, aboutFWBCommandID);
                commandService.AddCommand(aboutFWBMenuItem);
            }


        }
        private void AboutFWBHandler(object sender, EventArgs e)
        {
            // Logic for About FWB
            System.Windows.Forms.MessageBox.Show("About FWB Command Executed");
        }

        private void ShowToolWindow(object sender, EventArgs e)
        {
            // Get the instance of the Tool Window
            ToolWindowPane toolWindow = this.FindToolWindow(typeof(CreateEntity), 0, true);
            if (toolWindow?.Frame == null)
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            // Show the Tool Window
            IVsWindowFrame windowFrame = (IVsWindowFrame)toolWindow.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }
        #endregion
    }
}
