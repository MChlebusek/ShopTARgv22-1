using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Shop.ApplicationServices.Services;
using Shop.Core.ServiceInterface;
using Shop.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISpaceshipServices, SpaceshipServices>();
builder.Services.AddScoped<IFileServices, FilesServices>();
builder.Services.AddScoped<IRealEstatesServices, RealEstatesServices>();
//add dependence interface and service class
builder.Services.AddScoped<IKindergartenServices, KinderGartenServices>();
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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider
    (Path.Combine(builder.Environment.ContentRootPath, "multipleFileUpload")),
    RequestPath = "/multipleFileUpload"
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
