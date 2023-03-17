using ExamenWeb;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//Scaffold-DbContext name=DevConnection -Provider microsoft.EntityFrameworkCore.sqlserver -OutputDir Models -f 
//Conexion BD
var connectionString = builder.Configuration
    .GetConnectionString("DevConnection");

builder
    .Services
    .AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer(connectionString));


//add cors
builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Add services to the container.

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
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
