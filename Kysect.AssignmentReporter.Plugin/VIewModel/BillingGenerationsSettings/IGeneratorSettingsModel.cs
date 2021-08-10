using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kysect.AssignmentReporter.ReportGenerator;
using Microsoft.VisualStudio.PlatformUI;

namespace Kysect.AssignmentReporter.Plugin.VIewModel.BillingGenerationsSettings
{
    public interface IGeneratorSettingsModel
    {
        IReportGenerator Generator { get; set; }
        event EventHandler<GeneratorEventArgs> GeneratorUpdated;
        void UpdateGenerator(IReportGenerator generator);
    }
}
