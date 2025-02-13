using Assignment_WebBank.SnowFlake;
using Auth0.AspNetCore.Authentication;
using BankLibrary;
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

var builder = WebApplication.CreateBuilder(args);
var keyVaultName = "portfoliosecretsai.vault.azure.net";
var keyVaultUri = new Uri($"https://{keyVaultName}");

builder.Configuration
       .AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());


// Add services to the container.
builder.Services.AddSingleton<SnowflakeService>();
builder.Services.AddHttpClient<RandomAPIservice>();


builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
});

builder.Services.AddRazorPages();
builder.Services.AddResponseCaching();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


app.MapRazorPages();

app.UseResponseCaching();

app.Run();
