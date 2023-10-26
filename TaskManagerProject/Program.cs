using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerProject.Data;
using TaskManagerProject.Services;
using Microsoft.EntityFrameworkCore;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TaskManagerProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerProjectContext") ?? throw new InvalidOperationException("Connection string 'TaskManagerProjectContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<TaskManagerService>();

// Configure the reverse proxy
var proxyBuilder = builder.Services.AddReverseProxy();
proxyBuilder.LoadFromMemory(new[]
{
    new RouteConfig
    {
        RouteId = "api-route",
        ClusterId = "api-cluster",
        Match = new RouteMatch { Path = "/api/{**catch-all}" }
    }
}, new[]
{
    new ClusterConfig
    {
        ClusterId = "api-cluster",
        Destinations = new Dictionary<string, DestinationConfig>
        {
            ["api-destination"] = new DestinationConfig { Address = "https://s-install.avcdn.net/" }
        }
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

internal class Destination
{
    public string Address { get; set; }
}

internal class Cluster
{
    public string Id { get; set; }
    public Dictionary<string, Destination> Destinations { get; set; }
}

internal class ProxyMatch
{
    public string Path { get; set; }
}
