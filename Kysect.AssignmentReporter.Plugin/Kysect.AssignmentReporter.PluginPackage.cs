using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Threading;
using Task = System.Threading.Tasks.Task;

namespace Kysect.AssignmentReporter.Plugin
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    public sealed class AssignmentReporterPluginPackage : AsyncPackage
    {
        public const string PackageGuidString = "017225f8-7e93-42cb-84cc-9aaa284c6def";

    #region Package Members

    protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress) {
        await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
    }
    #endregion
}
}
