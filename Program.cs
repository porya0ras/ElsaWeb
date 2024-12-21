using Elsa.Dashboard.Extensions;
using Elsa.Extensions;
using ElsaTest.Workflows;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddElsa(elsa =>
{
    elsa.AddWorkflow<SaveForm>();
    elsa.UseHttp();
});

var app = builder.Build();

app.UseWorkflows();
app.MapGet("/", () => "Hello World!");
app.Run();
