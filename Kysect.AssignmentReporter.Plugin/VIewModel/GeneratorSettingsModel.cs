using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kysect.AssignmentReporter.Plugin.VIewModel.BillingGenerationsSettings;
using Kysect.AssignmentReporter.ReportGenerator;
using Microsoft.VisualStudio.PlatformUI;

namespace Kysect.AssignmentReporter.Plugin.VIewModel
{
    public class GeneratorSettingsModel : IGeneratorSettingsModel
    {
        public IReportGenerator Generator { get; set; }
        public event EventHandler<GeneratorEventArgs> GeneratorUpdated = delegate { };

        public GeneratorSettingsModel(IReportGenerator generator)
        {
            Generator = generator;
        }

        public void UpdateGenerator(IReportGenerator updatedGenerator)
        {
            Generator = updatedGenerator;
            GeneratorUpdated(this, new GeneratorEventArgs(updatedGenerator));
        }
    }
}
