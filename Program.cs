using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Configurar el esquema de autenticación
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie("UserScheme", options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.Cookie.Name = "UserAuthCookie";
    options.LoginPath = "/";
    options.AccessDeniedPath = "/error/access-denied";
})
.AddCookie("AdminScheme", options =>
{
    options.Cookie.Name = "AdminAuthCookie";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.LoginPath = "/login-admin";
    options.AccessDeniedPath = "/error/access-denied";
});

// Build the app
var app = builder.Build();

// Configuración de la cultura global
var supportedCultures = new[] { new CultureInfo("es-MX") };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("es-MX"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

app.UseRequestLocalization(localizationOptions);

// Set the path base from configuration
//var pathBase = builder.Configuration["PathBase"];
//if (!string.IsNullOrEmpty(pathBase))
//{
//    app.UsePathBase(pathBase);
//}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseAuthentication(); // Agregar el middleware de autenticación antes del middleware de autorización

app.UseStatusCodePages();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "admin",
    pattern: "administracion/{action=AdminHome}/{id?}",
    defaults: new { controller = "Admin" }
);

app.MapControllerRoute(
    name: "condusef",
    pattern: "condusef/{action=Home}/{id?}",
    defaults: new { controller = "Condusef" }
);

app.MapControllerRoute(
    name: "error",
    pattern: "error/{action=Index}/{id?}",
    defaults: new { controller = "Error" }
);

//app.UseSwagger();
//app.UseSwaggerUI();

app.Run();
