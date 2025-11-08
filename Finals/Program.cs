using Finals.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Finals.Data;
using Finals.Services;

var builder = WebApplication.CreateBuilder(args);

//sql lite connection
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add sales service
builder.Services.AddScoped<FeedbackService>();

// existing DbContext registration assumed present (UseSqlite or other)
builder.Services.AddScoped<OrderService>();

// ensure DbContext is registered; example with Sqlite:
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<AuthService>();

var app = builder.Build();

// seed admin user (if you want an admin available)
using (var scope = app.Services.CreateScope())
{
    var auth = scope.ServiceProvider.GetRequiredService<AuthService>();
    // create admin if not present
    auth.RegisterUserAsync("admin", "admin@example.com", "AdminP@ss123", isAdmin: true).GetAwaiter().GetResult();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();