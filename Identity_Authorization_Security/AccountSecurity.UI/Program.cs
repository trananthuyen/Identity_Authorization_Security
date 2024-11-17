using AccountManager.Core.Domain.IdentifyEntities;
using AccountSecurity.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
     options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

/*
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>().AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>(); //enable identity in this project (ho tro xac thuc tai khoan)
*/
//deteal config

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{ //set dk toi thieu ve mat khau
    options.Password.RequiredLength = 5; //mat khau toi thieu la 5 ki tu
    options.Password.RequireNonAlphanumeric = false; //ko bat buoc mk phai la chu hay ki tu
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 3; //eg: AB12AB(unique characters are A, B, 1, 2) 3 ki tu doc  nhat tro len

    
}).AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>().AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();



app.UseRouting();

app.UseAuthorization();


//app.UseAuthorization(); //de su dung tat ca chuc nang can phai dang nhap //validates access permissions of the user

app.MapControllers();

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
  ///////// OR /////////////
  [Route("[Controller]/[action]")] IN CONTROLLER

  2 CACH NAY DEU DUOC SUC DUNG DE DINH TUYEN
 
 */


app.Run();
