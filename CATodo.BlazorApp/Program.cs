using CATodo.BlazorApp;
using CATodo.BlazorApp.Services;
using CATodo.BLLContracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ICATodoService>(sp =>
    new TodoService(
        new HttpClient { 
            BaseAddress = new Uri(sp.GetRequiredService<IConfiguration>().GetValue<string>("TodoWebApi:Url")) 
        }
    )
);

await builder.Build().RunAsync();
