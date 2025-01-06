using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace FWB.PowerTool.Views
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("b1b63e0f-ef24-454c-b8f0-bcc13a7cab22")]
    public class CreateEntity : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEntity"/> class.
        /// </summary>
        public CreateEntity() : base(null)
        {
            this.Caption = "CreateEntity";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new CreateEntityControl();
        }
    }
}
