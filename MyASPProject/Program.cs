using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyASPProject.Data;
using MyASPProject.Models;
using MyASPProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//menambahkan pengaturan Identity
builder.Services.AddIdentity<CustomIdentityUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 10;
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireNonAlphanumeric = false;

}).AddDefaultUI().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<RestaurantDbContext>();

//Session
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "mysession.fronted";
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.IsEssential = true;
});


//menambahkan agar pengguna yang belum login diminta login dan dapat mengakses controller
builder.Services.ConfigureApplicationCookie(opt => opt.LoginPath = "/Account/Login");

/*builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<RestaurantDbContext>();*/

builder.Services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));



//Menambahkan Claims yang berfungsi untuk meng authorize controller yang kita punya
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim("Delete Role"));
    //var policy = new AuthorizationPolicyBuilder
});



//Daftarkan Service
//builder.Services.AddScoped<IPengguna, PenggunaServices>();
builder.Services.AddScoped<IRestaurantData, SqlRestaurantData>();

builder.Services.AddScoped<ISamurai, SamuraiServices>();

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
app.UseAuthentication();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
