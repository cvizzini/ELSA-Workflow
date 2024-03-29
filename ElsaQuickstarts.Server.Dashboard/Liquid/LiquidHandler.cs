﻿using System.Threading;
using System.Threading.Tasks;
using Elsa.Scripting.Liquid.Messages;
using ElsaQuickstarts.Server.Dashboard.Models;
using Fluid;
using MediatR;

namespace ElsaQuickstarts.Server.Dashboard.Liquid
{
    public class MyTypeLiquidHandler : INotificationHandler<EvaluatingLiquidExpression>
    {
        public Task Handle(EvaluatingLiquidExpression notification, CancellationToken cancellationToken)
        {
            notification.TemplateContext.Options.MemberAccessStrategy.Register<FileModel>();
            return Task.CompletedTask;
        }
    }
}
