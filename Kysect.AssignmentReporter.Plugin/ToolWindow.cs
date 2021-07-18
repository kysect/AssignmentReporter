using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace Kysect.AssignmentReporter.Plugin
{
    public class ToolWindow : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindow"/> class.
        /// </summary>
        
        public ToolWindow() : base(null)
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ToolWindow"/> class.
            /// </summary>
            Caption = "AssignmentReporter";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            Content = new ToolWindowControl();
        }
    }
}
