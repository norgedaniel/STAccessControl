using Microsoft.EntityFrameworkCore;
using STCA_DataLayer;
using STCA_ServiceLayer;



var builder = WebApplication.CreateBuilder(args);


var connection = @"Data Source = (localdb)\mssqllocaldb; Initial Catalog = STCA_DEMO; Integrated Security = True";

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<TiposAreasAccesoListService>();


// Add services to the container.
builder.Services.AddRazorPages();

// setting RouteOptions to always get the URL in lowercase and ending with "/"
builder.Services.Configure<RouteOptions>(options =>
{
    options.AppendTrailingSlash = true;
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
