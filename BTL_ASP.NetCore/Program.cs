using BTL_ASP.NetCore.Areas.Admin.Models.DataModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ConnectDb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connect")));


// cấu hình lưu session
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);// luu 30 phut
    options.Cookie.IsEssential = true;


});
//c?u hình xác th?c ng??i dùng b?ng Cookie
builder.Services.AddAuthentication("CookieAuthenication").AddCookie("CookieAuthenication", options =>
{
    //ch? ra ???ng d?n c?m truy c?p m?c dù ?ã ??ng nh?p
    //option.AccessDeniedPath = new PathString( "/Manager/Account");
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = "c2208i",
        Path = "/"
    };

    //ch? ??nh ???ng d?n
    options.LoginPath = new PathString("/Admin/Login/Index");
    options.ReturnUrlParameter = "UrlRedirect";
    options.SlidingExpiration=true;
       

});

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

app.UseSession();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
