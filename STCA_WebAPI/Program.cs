using Microsoft.EntityFrameworkCore;
using STCA_DataLayer;
using STCA_ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = @"Data Source = (localdb)\mssqllocaldb; Initial Catalog = STCA_DEMO; Integrated Security = True";

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<TiposAreasAccesoListService>();
 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
