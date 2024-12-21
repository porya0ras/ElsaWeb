using Elsa.Expressions.Models;
using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Models;
using ElsaWeb.Models;
using ElsaWeb.Services;
using ElsaWeb.Services.Abstracts;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace ElsaWeb.CustomActivities;

public class CreateRequestInDb: CodeActivity
{
    private IDataAccessService _dataAccessService;
    public Input<Request> Request { get; set; } = default!;

    public CreateRequestInDb(Func<ExpressionExecutionContext, Request?> req)
    {
        Request = new Input<Request>(Expression.DelegateExpression(req), new MemoryBlockReference());
    }

    protected override void Execute(ActivityExecutionContext context)
    {
        _dataAccessService = context.GetRequiredService<IDataAccessService>();
        //add request to db 
        var request = context.Get(Request);

        var res=_dataAccessService.AddRequest(request).Result;
        if (!res)
        {
            context.CreateVariable("force_reject", true);
        }
        else
        {
            context.CreateVariable("requestId", request?.Id);
            context.CreateVariable("force_reject", false);
        }
    }
}
