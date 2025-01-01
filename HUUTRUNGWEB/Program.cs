using HUUTRUNG.DataAccess.Repository;
using HUUTRUNG.DataAccess.Repository.IRepository;
using HUUTRUNG.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HUUTRUNG.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using HUUTRUNG.DataAccess.DbInitializer;
using HUUTRUNG.Models.Domain;
using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe")); 


//builder.Services.AddIdentity<IdentityUser,IdentityRole>  
//    (options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); //Thêm IdentityRole

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); //Thêm IdentityRole



builder.Services.ConfigureApplicationCookie(options =>  // phai them sau xac thuc danh tinh
{
	options.LoginPath = $"/Identity/Account/Login";
	options.LogoutPath = $"/Identity/Account/Logout";
	options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

// session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});




//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); // controller trien khai ic thi dk o day
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
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
StripeConfiguration.ApiKey=builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();


app.UseRouting();
app.UseSession();
SeedDatabase();
app.UseAuthentication(); // xác thực ủy quyen
app.UseAuthorization(); 
app.MapRazorPages();


app.MapControllerRoute(
    name: "default",
	pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
 //   pattern: "{controller=Home}/{action=Index}/{id?}");
    

app.Run();      
void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}