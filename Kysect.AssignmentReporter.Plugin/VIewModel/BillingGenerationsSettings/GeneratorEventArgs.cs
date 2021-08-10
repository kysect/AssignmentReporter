using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kysect.AssignmentReporter.ReportGenerator;

namespace Kysect.AssignmentReporter.Plugin.VIewModel.BillingGenerationsSettings
{
    public class GeneratorEventArgs : EventArgs
    {
        public IReportGenerator Generator { get; set; }

        public GeneratorEventArgs(IReportGenerator generator)
        {
            Generator = generator;
        }
    }
}
