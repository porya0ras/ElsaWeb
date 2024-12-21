using Elsa.Http;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Activities.Flowchart.Activities;
using Elsa.Workflows.Contracts;
using Elsa.Workflows.Memory;
using ElsaWeb.Models;
using System.Dynamic;

namespace ElsaWeb.WorkFlows;

public class SaveForm: WorkflowBase
{
    protected override void Build(IWorkflowBuilder builder)
    {
        var requestVariable = builder.WithVariable<Request>();

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
            }
        };
    }
}
