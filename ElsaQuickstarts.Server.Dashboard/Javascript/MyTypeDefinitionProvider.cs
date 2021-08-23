using Elsa.Scripting.JavaScript.Services;
using ElsaQuickstarts.Server.Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ElsaQuickstarts.Server.Dashboard.Javascript
{

    //Allow Dashboard intelisense understand the file model
    public class MyTypeDefinitionProvider : TypeDefinitionProvider
    {
        public override ValueTask<IEnumerable<Type>> CollectTypesAsync(TypeDefinitionContext context, CancellationToken cancellationToken = default)
        {
            var types = new[] { typeof(FileModel) };
            return new ValueTask<IEnumerable<Type>>(types);
        }
    }
}
