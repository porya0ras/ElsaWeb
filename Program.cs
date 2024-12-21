using Elsa.Dashboard.Extensions;
using Elsa.Extensions;
using ElsaWeb;
using ElsaWeb.Services;
using ElsaWeb.Services.Abstracts;
using ElsaWeb.WorkFlows;
using Microsoft.EntityFrameworkCore;
using Parlot.Fluent;
using System;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDbContext<MainDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
builder.Services.AddElsa(elsa =>
{
    // workflows by assembly
    elsa.AddWorkflowsFrom<Program>();
    //elsa.UseWorkflowsApi();

    elsa.UseHttp();
    elsa.UseSasTokens();
});

builder.Services.AddScoped<IDataAccessService, DataAccessService>();

var app = builder.Build();

// Expose Elsa APIs.
//app.UseWorkflowsApi();

// Add Elsa HTTP middleware (to handle requests mapped to HTTP Endpoint activities)
app.UseWorkflows();

app.MapGet("/", () => "Hello World!");
app.MapGet("/requests", async (MainDbContext db) => await db.Items.ToListAsync());
app.Run();
