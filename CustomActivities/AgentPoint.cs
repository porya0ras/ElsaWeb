using Elsa.Expressions.Models;
using Elsa.Extensions;
using Elsa.Http;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Memory;
using Elsa.Workflows.Runtime.Activities;

namespace ElsaWeb.CustomActivities;

public class AgentPoint : Sequence
{

    Activity approveURL;
    Activity rejectURL;

    public AgentPoint(Variable<bool> approvedVariable)
    {
        approveURL = new WriteLine(context => $"Jack approve url: \n {GenerateSignalUrl(context, "Approve:Jack")}");
        rejectURL = new WriteLine(context => $"Jack reject url: \n {GenerateSignalUrl(context, "Reject:Jack")}");
        Activities.Add(approveURL);
        Activities.Add(rejectURL);
        Activities.Add(new Fork
        {
            JoinMode = ForkJoinMode.WaitAny,
            Branches =
            {
                // Approve
                new Sequence
                {
                    Activities =
                    {
                        new Event("Approve:Jack"),
                        new SetVariable
                        {
                            Variable = approvedVariable,
                            Value = new(true)
                        },
                        new WriteHttpResponse
                        {
                            Content = new("Thanks for the approval, Jack!"),
                        }
                    }
                },
                // Reject
                new Sequence
                {
                    Activities =
                    {
                        new Event("Reject:Jack"),
                        new SetVariable
                        {
                            Variable = approvedVariable,
                            Value = new(false)
                        },
                        new WriteHttpResponse
                        {
                            Content = new("Sorry to hear that, Jack!"),
                        }
                    }
                }
            }
        });
    }

    private string GenerateSignalUrl(ExpressionExecutionContext context, string signalName)
    {
        return context.GenerateEventTriggerUrl(signalName);
    }

}
