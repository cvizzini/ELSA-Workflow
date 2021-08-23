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
       DisplayName = "Write Hello World",
       Description = "Write Hello World to standard out.",
       Outcomes = new[] { OutcomeNames.Done }
   )]
    public class SayHelloWorld : Activity
    {
        private readonly TextWriter _writer;

        public SayHelloWorld(TextWriter writer)
        {
            _writer = writer;
        }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            await _writer.WriteLineAsync("Hello World!");

            return Done();
        }
    }
}
