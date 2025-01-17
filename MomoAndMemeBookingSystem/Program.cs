using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MomoAndMemeBookingSystem.Data;
using MomoAndMemeBookingSystem.Models;
using MomoAndMemeBookingSystem.Services;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity.UI.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Set the connection string for PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddTransient<EmailService>();




// Configure Entity Framework to use PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Enable developer exception pages for migrations
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add Identity services for user authentication and registration
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add MVC controllers and views
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!context.MassageTypes.Any())
    {
        context.MassageTypes.AddRange(
            new MassageType { Name = "Thai", Price = 5000 },
            new MassageType { Name = "Relaxation", Price = 4000 }
        );
    }

    if (!context.Masseurs.Any())
    {
        context.Masseurs.AddRange(
            new Masseur { FullName = "John Doe", Bio = "Expert in Thai massage" ,Image="1.png"},
            new Masseur { FullName = "Jane Smith", Bio = "Specializes in relaxation techniques",Image="2.jpeg" }
        );
    }

    context.SaveChanges();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Use developer-friendly error pages during development
    app.UseMigrationsEndPoint();
}
else
{
    // Use a generic error page for production
    app.UseExceptionHandler("/Home/Error");
    // Enable HSTS for security
    app.UseHsts();
}

// Configure HTTPS redirection and static file serving
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.UseAuthorization();

// Map default controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map Razor Pages
app.MapRazorPages();

app.Run();
