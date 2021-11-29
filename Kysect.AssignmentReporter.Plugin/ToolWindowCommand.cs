using Task = System.Threading.Tasks.Task;

namespace Kysect.AssignmentReporter.Plugin
{
    internal sealed class ToolWindowCommand
    {
        public const int CommandId = 0x0100;

        public static readonly Guid CommandSet = new Guid("eaab0023-00ec-4b84-af3d-99da96f93701");

        private readonly AsyncPackage _package;

        private ToolWindowCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            _package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(Execute, menuCommandId);
            commandService.AddCommand(menuItem);
        }

        public static ToolWindowCommand Instance { get; private set; }

        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider { get => _package; }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService =
                await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new ToolWindowCommand(package, commandService);
        }

        private void Execute(object sender, EventArgs e)
        {
            _package.JoinableTaskFactory.RunAsync(async delegate
            {
                ToolWindowPane window =
                    await _package.ShowToolWindowAsync(typeof(ToolWindow), 0, true, _package.DisposalToken);
                if (null == window || null == window.Frame){
                    throw new NotSupportedException("Cannot create tool window");
                }
            });
        }
    }
}