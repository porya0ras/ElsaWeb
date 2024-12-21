using Elsa.Dashboard.Extensions;
using Elsa.Extensions;
using ElsaWeb;
using ElsaWeb.Services;
using ElsaWeb.Services.Abstracts;
using ElsaWeb.WorkFlows;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MainDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
builder.Services.AddElsa(elsa =>
{
    // workflows by assembly
    elsa.AddWorkflowsFrom<Program>();
    elsa.UseHttp();
});

builder.Services.AddScoped<IDataAccessService, DataAccessService>();

var app = builder.Build();

app.UseWorkflows();

app.MapGet("/", () => "Hello World!");
app.MapGet("/requests", async (MainDbContext db) => await db.Items.ToListAsync());
app.Run();
