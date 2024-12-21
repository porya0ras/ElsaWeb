using Elsa.Expressions.Models;
using Elsa.Extensions;
using Elsa.Http;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Activities.Flowchart.Activities;
using Elsa.Workflows.Contracts;
using Elsa.Workflows.Memory;
using ElsaWeb.CustomActivities;
using ElsaWeb.Models;
using System.Dynamic;

namespace ElsaWeb.WorkFlows;

public class SaveForm : WorkflowBase
{
    protected override void Build(IWorkflowBuilder builder)
    {
        var requestVariable = builder.WithVariable<Request>();

        var _staticActivities = new Sequence
        {
            Activities =
            {
                new WriteLine("static activities!")
            }
        };

        var _Activities = new Sequence
        {
            Activities =
            {
                // add dynamic tenant flow
                new WriteLine("Request saved!"),
                _staticActivities
            }
        };


        builder.Root = new Sequence
        {
            Activities =
            {
                new HttpEndpoint
                {
                    Path = new("/save-form"),
                    CanStartWorkflow = true,
                    ParsedContent = new(requestVariable),
                    SupportedMethods = new(new[] { HttpMethods.Post }),
                },
               new WriteLine(context => $"Request received from {requestVariable.Get<Request>(context)!.Owner}."),
               new CreateRequestInDb(context =>requestVariable.Get<Request>(context)),
               new If(context =>
               context.GetVariable<bool>("force_reject")
               )
               {
                   Then=new WriteLine("Request auto rejected!"),
                   Else=_Activities,
               },
            }
        };
    }

    private string GenerateSignalUrl(ExpressionExecutionContext context, string signalName)
    {
        return context.GenerateEventTriggerUrl(signalName);
    }

    private bool ValidateRequest(ExpressionExecutionContext context)
    {

        return false;
    }
}
