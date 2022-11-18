using GameReviewer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ReviewContext>(x => x.UseSqlServer(
        "Server=DESKTOP-EII9684;Database=GameReviewer;Trusted_Connection=True;"
    ));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IRepository<Producer>, ProducerRepository>();
builder.Services.AddSingleton<IRepository<Game>, GameRepository>();
builder.Services.AddSingleton<IRepository<Image>, ImageRepository>();
builder.Services.AddSingleton<IRepository<Review>, ReviewsRepository>();
builder.Services.AddSingleton<IUserRepository<AppUser>, UserRepository>();
builder.Services.Configure<PasswordHasherOptions>(options => options.IterationCount = 512000);
//Sesja
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(
    options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(10);
        options.Cookie.Name = "GameReviewerCookie";
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });
builder.Services.AddMemoryCache();

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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
