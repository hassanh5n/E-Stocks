using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication2.Data;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);

// Get connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add MySQL DbContext
builder.Services.AddDbContext<EstocksDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register StockDataService
builder.Services.AddScoped<StockDataService>();

// Add MVC services (controllers + views)
builder.Services.AddControllersWithViews();

// Add cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login"; // redirect unauthenticated users to login
        options.LogoutPath = "/Users/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(14);
    });

var app = builder.Build();

// Configure middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Map the default route: /{controller=Home}/{action=Index}/{id?}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Auto-create database on startup with Retry Logic
using (var scope = app.Services.CreateScope())
{
    var retries = 10;
    while (retries > 0)
    {
        try
        {
            Console.WriteLine($"[Program] Connecting to database (Attempt {11 - retries}/10)...");
            var context = scope.ServiceProvider.GetRequiredService<EstocksDbContext>();
            context.Database.EnsureCreated();
            Console.WriteLine("Database connected and created successfully!");
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database connection failed: {ex.Message}");
            retries--;
            if (retries > 0)
            {
                Console.WriteLine("Waiting 3 seconds before retry...");
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                Console.WriteLine("Exhausted all retries. Exiting.");
                // We let it crash so the container stops and doesn't sit in a broken state
                throw;
            }
        }
    }
}

app.Run();
