var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.WebHost.UseWebRoot("wwwroot");
builder.WebHost.UseUrls("https://localhost:2211");

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
       name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();