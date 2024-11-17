var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers();

var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();


app.MapControllers();

app.UseAuthorization();



app.Run();
