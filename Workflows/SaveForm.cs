using Elsa.Http;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Activities.Flowchart.Activities;
using Elsa.Workflows.Contracts;

namespace ElsaTest.Workflows;

public class SaveForm
: WorkflowBase
{
    protected override void Build(IWorkflowBuilder builder)
    {

        builder.Root = new Sequence
        {
            Activities =
            {
                new HttpEndpoint
                {
                    Path = new("/save-form"),
                    CanStartWorkflow = true,
                },
                new WriteHttpResponse
                {
                    Content = new("Hello world of HTTP workflows!")
                },
                new HttpEndpoint
                {
                    Path = new("/hello-world-2"),
                },
                new WriteLine("hello-world-3"),
                new HttpEndpoint
                {
                    Path = new("/hello-world-4"),
                },
                new Sequence
                {
                    Activities =
                    {
                        new HttpEndpoint
                        {
                            Path = new("/flow-1"),
                        },
                         new HttpEndpoint
                        {
                            Path = new("/flow-2"),
                        },
                          new WriteLine("flow-done"),
                    }
                },
                new WriteHttpResponse
                {
                    Content = new("Hello world of HTTP workflows5!")
                },
            }
        };
    }
}
