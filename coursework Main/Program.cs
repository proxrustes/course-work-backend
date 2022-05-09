using DBAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

//add services 
builder.Services.AddRazorPages();
builder.Services.AddTransient<IRepositories<RepositoryPerson>>();
builder.Services.AddTransient<IRepositories<RepositoryUser>>();
builder.Services.AddTransient<IRepositories<RepositoryImage>>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

public static IWebHost BuildWebHost(string[] args)
{
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .Build();
}