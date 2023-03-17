global using ClassLibraryInfinity.DTOs;
global using Infinity.UI.Shared.Modals;
global using Blazored.LocalStorage;
global using Microsoft.AspNetCore.Components.Authorization;
using Infinity.UI;
using Infinity.UI.Services.AuthService;
using Infinity.UI.Services.UserService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Infinity.UI.Services.PublicationsService;
using Infinity.UI.Services.CommentService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPublicationService, PublicationService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddOptions();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("user", policy => policy.RequireClaim("user"));
});

await builder.Build().RunAsync();
