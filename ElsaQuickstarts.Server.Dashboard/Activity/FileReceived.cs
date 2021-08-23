using Elsa;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Services.Models;
using ElsaQuickstarts.Server.Dashboard.Models;

namespace ElsaQuickstarts.Server.Dashboard.Activity
{
    [Trigger(
       Category = "Demo",
       Description = "Triggers when a file is received",
       DisplayName = "File Received",    
       Outcomes = new[] { OutcomeNames.Done, "Suspend" }
   )]
    public class FileReceived : Activity
    {
        [ActivityOutput] public FileModel Output { get; set; }

        protected override IActivityExecutionResult OnExecute(ActivityExecutionContext context)
        {
            return context.WorkflowExecutionContext.IsFirstPass ? OnExecuteInternal(context) : Suspend();
        }

        protected override IActivityExecutionResult OnResume(ActivityExecutionContext context)
        {
            return OnExecuteInternal(context);
        }

        private IActivityExecutionResult OnExecuteInternal(ActivityExecutionContext context)
        {
            var file = context.GetInput<FileModel>();
            Output = file;
            return Done();
        }
    }
}
