using System.Net.Http.Headers;
using ApiRestProject.Business;
using ApiRestProject.Business.Impl;
using ApiRestProject.Hypermedia.Enricher;
using ApiRestProject.Hypermedia.Filters;
using ApiRestProject.Model.Context;
using ApiRestProject.Repository.Generic;
using EvolveDb;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    connection, new MySqlServerVersion(new Version(8, 0, 46))));

if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection);
}

builder.Services.AddApiVersioning();
//Injecao de dependencia
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImpl>();

builder.Services.AddScoped<IBookBusiness, BookBusinessImpl>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

builder.Services.AddMvc(options =>
{
  options.RespectBrowserAcceptHeader = true;
  options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
  options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/json");
})
.AddXmlSerializerFormatters();

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

builder.Services.AddSingleton(filterOptions);

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
app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

app.Run();

void MigrateDatabase(string? connection)
{
  try
  {
    var evolveConnection = new MySqlConnection(connection);
    var evolve = new Evolve(evolveConnection, Log.Information)
    {
        Locations = new List<string> {"db/migrations", "db/dataset"},
        IsEraseDisabled = true,
    };

    evolve.Migrate();

  }
  catch (Exception ex)
  {
    Log.Error("Database Migration Error", ex);
    throw;
  }
}