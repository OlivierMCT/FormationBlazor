using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SayHelloBlazorApp;
using SayHelloBlazorApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<IBonjourService, BonjourService>();
builder.Services.AddSingleton<IStyleService, StyleService>();

builder.Services.AddSingleton(sp => {
    var http = new HttpClient() { BaseAddress = new Uri("https://parseapi.back4app.com/classes/Color") };
    http.DefaultRequestHeaders.Add("X-Parse-Application-Id", "vei5uu7QWv5PsN3vS33pfc7MPeOPeZkrOcP24yNX");
    http.DefaultRequestHeaders.Add("X-Parse-Master-Key", "aImLE6lX86EFpea2nDjq9123qJnG0hxke416U7Je");
    return http;
});

await builder.Build().RunAsync();
