using System.IO;
using System.Threading.Tasks;
using Elsa;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Services.Models;


namespace ElsaQuickstarts.Server.Dashboard.Activity
{
    [Activity(
        Category = "Demo",
        DisplayName = "Write Line Custom",
        Description = "Write text to standard out.",
        Outcomes = new[] { OutcomeNames.Done }
    )]
    public class WriteLineActivity : Activity
    {
        private readonly TextWriter _writer;

        public WriteLineActivity(TextWriter writer)
        {
            _writer = writer;
        }

        [ActivityInput(Hint = "The message to write.")]
        public string Message { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            await _writer.WriteLineAsync(Message);

            return Done();
        }
    }
}
