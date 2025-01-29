using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GymProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options => {
    options.Conventions.AuthorizeFolder("/WeightEvolutions");
    options.Conventions.AuthorizeFolder("/Workouts");
    options.Conventions.AuthorizeFolder("/Exercises");
    options.Conventions.AllowAnonymousToPage("/Exercises/Index");
    options.Conventions.AllowAnonymousToPage("/Exercises/Details");
});
builder.Services.AddDbContext<GymProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GymProjectContext") ?? throw new InvalidOperationException("Connection string 'GymProjectContext' not found.")));

builder.Services.AddDbContext<_LibraryIdentityContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("GymProjectContext") ?? throw new InvalidOperationException("Connection string 'GymProjectContext' not found."))); 

builder.Services.AddDefaultIdentity<IdentityUser>(options => 
    options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<_LibraryIdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
