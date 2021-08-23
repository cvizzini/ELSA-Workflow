using Elsa.Activities.Console;
using Elsa.Builders;

namespace ElsaQuickstarts.Server.Dashboard.Workflows
{
    /// <summary>
    /// A basic workflow with just one WriteLine activity.
    /// </summary>
    public class HelloWorldConsoleWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder) => builder.WriteLine("Hello World!");
    }
}
