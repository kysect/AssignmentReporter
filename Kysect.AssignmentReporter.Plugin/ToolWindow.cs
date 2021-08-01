using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Kysect.AssignmentReporter.Plugin.Windows;
using Microsoft.VisualStudio.Shell;

namespace Kysect.AssignmentReporter.Plugin
{
    [Guid("876b826b-f4eb-4107-bca7-829a63ea2037")]
    public class ToolWindow : ToolWindowPane
    {
        public ToolWindow() : base(null)
        {
            Caption = "AssignmentReporter";
            Content = new ToolWindowControl();
        }
    }
}
