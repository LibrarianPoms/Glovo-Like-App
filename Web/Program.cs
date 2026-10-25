using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Aymane.netProjet.Data;

var builder = WebApplication.CreateBuilder(args);

// Set default culture for currency and formatting
var cultureInfo = new System.Globalization.CultureInfo("fr-FR");
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add services to the container.
builder.Services.AddScoped<Aymane.netProjet.Repos.ProduitRepos>();
builder.Services.AddScoped<Aymane.netProjet.Services.ProduitService>();

builder.Services.AddScoped<Aymane.netProjet.Repos.ClientRepos>();
builder.Services.AddScoped<Aymane.netProjet.Services.ClientService>();

builder.Services.AddScoped<Aymane.netProjet.Repos.CommandeRepos>();
builder.Services.AddScoped<Aymane.netProjet.Services.CommandeService>();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyDbContextGlovo>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("myCnn")));

builder.Services.AddSession();




var app = builder.Build();

// Apply pending migrations at startup so the database schema (e.g. Client.MotPasse) matches the EF model
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContextGlovo>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
