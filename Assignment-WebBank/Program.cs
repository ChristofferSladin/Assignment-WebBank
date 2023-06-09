using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Data;
using Assignment_WebBank.Services;
using BankLibrary.Services;
using BankLibrary.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddTransient<DataInitializer>();

builder.Services.AddHttpClient<IRandomAPIservice, RandomAPIservice>();

// L�gg till min DbContext
builder.Services.AddDbContext<BankAppDataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// L�gg till min IndexService
builder.Services.AddTransient<IIndexService, IndexService>();

// L�gg till min SupplierService
builder.Services.AddTransient<ICustomerService, CustomerService>();

//L�gg till min TransactionService
builder.Services.AddTransient<ITransactionService, TransactionService>();

//L�gg till min CRUDService
builder.Services.AddTransient<ICRUDservice, CRUDservice>();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(ValidateCustomerVM)));

builder.Services.AddResponseCaching();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<DataInitializer>()?.SeedData();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.UseResponseCaching();

app.Run();
