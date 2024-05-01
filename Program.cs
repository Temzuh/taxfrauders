using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = "server=172.17.0.1;user=root;password=taxfrauders;database=taxfrauders;port=6650";
// Replace with your server version and type.
// Use 'MariaDbServerVersion' for MariaDB.
// Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
// For common usages, see pull request #1233.
var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Login";
		options.AccessDeniedPath = "/Login/Logout";
		options.LogoutPath = "/";
		options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
		options.Cookie.Name = "PERSEHIKI";
	});
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("NoAdminAccess", policy =>
	{
		policy.RequireAssertion(context =>

			!(context.User.Identity.IsAuthenticated && context.User.IsInRole("admin")));

	});
});
// Replace 'YourDbContext' with the name of your own DbContext derived class.
builder.Services.AddDbContext<TaxfraudersDbContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion)
        // The following three options help with debugging, but should
        // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
