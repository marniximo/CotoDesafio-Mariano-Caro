using CotoDesafio.Infrastructure;
using CotoDesafio.Infrastructure.Interfaces;
using CotoDesafio.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/*
    Una fábrica de automóviles produce 4 modelos de coches (sedan, suv, offroad, sport) cuyos precios de venta son: 8.000 u$s, 9.500 u$s, 12.500 u$s y 18.200 u$s. 
    La empresa tiene 4 centros de distribución y venta. Se tiene una relación de datos correspondientes al tipo de vehículo vendido y punto de distribución en el que se produjo la venta del mismo.
    El tipo “sport” incluye un impuesto extra del 7% que se debe adicionar al precio en la venta.
    Realizar una api rest que contemple:
•            Insertar una venta
•            Obtener el volumen de ventas total.
•            Obtener el volumen de ventas por centro.
•            Obtener el porcentaje de unidades de cada modelo vendido en cada centro sobre el total de ventas de la empresa.
 */

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

app.UseAuthorization();

app.MapControllers();

app.Run();
