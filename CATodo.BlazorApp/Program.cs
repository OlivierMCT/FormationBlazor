using CATodo.BlazorApp;
using CATodo.BlazorApp.Services;
using CATodo.BLLContracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<ICATodoService>(sp =>
    new TodoService(
        new HttpClient { 
            BaseAddress = new Uri(sp.GetRequiredService<IConfiguration>().GetValue<string>("TodoWebApi:Url")) 
        }
    )
);
builder.Services.AddTransient(sp => 
    new HubConnectionBuilder()
        .WithUrl(sp.GetRequiredService<IConfiguration>().GetValue<string>("TodoWebApi:Url") + "todo-hub")
        .WithAutomaticReconnect()
        .Build()
);


await builder.Build().RunAsync();
