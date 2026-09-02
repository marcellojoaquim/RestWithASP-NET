using System.Text;
using ApiRestProject.Business;
using ApiRestProject.Business.Impl;
using ApiRestProject.Configurations;
using ApiRestProject.Hypermedia.Enricher;
using ApiRestProject.Hypermedia.Filters;
using ApiRestProject.Model.Context;
using ApiRestProject.Repository;
using ApiRestProject.Repository.Generic;
using ApiRestProject.Services;
using ApiRestProject.Services.Impl;
using EvolveDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var tokenConfiguration = new TokenConfiguration();
var configurationToken = builder.Configuration["TokenConfigurations"];

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = tokenConfiguration.Issuer,
    ValidAudience = tokenConfiguration.Audience,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Secret))
  };
});

builder.Services.AddAuthorization(auth =>
{
  auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
  .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
  .RequireAuthenticatedUser().Build());
});

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(builder =>
  {
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
  });
});

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
builder.Services.AddScoped<ILoginBusiness, LoginBusinessImpl>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

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
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1",
  new OpenApiInfo
  {
    Title = "Rest API",
    Description = "Curso DotNet - Construindo Restful API's",
    Contact = new OpenApiContact
    {
      Name = "Marcello",
      Email = "",
      Url = new Uri("https://github.com/marcellojoaquim/")
    }
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso DotNet - Construindo Restful API's");
  });
}

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseHttpsRedirection();
app.UseCors();
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
      Locations = new List<string> { "db/migrations", "db/dataset" },
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