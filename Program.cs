using Microsoft.EntityFrameworkCore;
using LicenciasAPI.Data;
//using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddControllers();

// Cambia o agrega esta sección para configurar Swagger/OpenAPI según tus necesidades
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); //Habilita el middleware de Swagger para generar el JSON de la API
    app.UseSwaggerUI();//Habilita la interfaz de usuario de Swagger para probar la API desde el navegador
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
