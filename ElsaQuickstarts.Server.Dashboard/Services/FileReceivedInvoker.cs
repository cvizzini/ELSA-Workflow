using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Models;
using Elsa.Services;
using Elsa.Services.Models;
using ElsaQuickstarts.Server.Dashboard.Activity;
using ElsaQuickstarts.Server.Dashboard.Bookmarks;
using ElsaQuickstarts.Server.Dashboard.Models;

namespace ElsaQuickstarts.Server.Dashboard.Services
{
    public class FileReceivedInvoker : IFileReceivedInvoker
    {
        private readonly IWorkflowLaunchpad _workflowLaunchpad;

        public FileReceivedInvoker(IWorkflowLaunchpad workflowLaunchpad)
        {
            _workflowLaunchpad = workflowLaunchpad;
        }

        public async Task<IEnumerable<CollectedWorkflow>> DispatchWorkflowsAsync(FileModel file, CancellationToken cancellationToken = default)
        {
            var context = new WorkflowsQuery(nameof(FileReceived), new FileReceivedBookmark());
            var input = new WorkflowInput(file);
            return await _workflowLaunchpad.CollectAndDispatchWorkflowsAsync(context, input, cancellationToken);
        }

        public async Task<IEnumerable<CollectedWorkflow>> ExecuteWorkflowsAsync(FileModel file, CancellationToken cancellationToken = default)
        {
            var context = new WorkflowsQuery(nameof(FileReceived), new FileReceivedBookmark());
            var input = new WorkflowInput(file);
            return await _workflowLaunchpad.CollectAndExecuteWorkflowsAsync(context, input, cancellationToken);
        }
    }
}