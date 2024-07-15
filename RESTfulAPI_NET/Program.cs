using EvolveDb;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using RESTfulAPI_NET.Business;
using RESTfulAPI_NET.Business.Implementations;
using RESTfulAPI_NET.Hypermedia.Enricher;
using RESTfulAPI_NET.Hypermedia.Filters;
using RESTfulAPI_NET.Model.Context;
using RESTfulAPI_NET.Repository;
using RESTfulAPI_NET.Repository.Generic;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var appName = "RESTfulAPI_NET ASP.NET 8 and Docker and Github Actions";
var appVersion = "v1";
var appDescription = "API RESTful developed in ASP.NET Core 8";

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c => {
	c.SwaggerDoc(appVersion, new OpenApiInfo
	{
		Title = appName,
		Version = appVersion,
		Description = appDescription,
		Contact = new OpenApiContact
		{ 
			Name = "Geraldo Luiz",
			Url = new Uri("https://github.com/geraldobl58")
		}
    });
});

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => 
{
	builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var connection = builder.Configuration["MySQLConnection:MySQLConnectionStribng"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    connection,
    new MySqlServerVersion(new Version(8,0,2))
));

if (builder.Environment.IsDevelopment())
{
    MigrationDatabase(connection);
}

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
builder.Services.AddSingleton(filterOptions);

// Versioning API
builder.Services.AddApiVersioning();

// Add Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", appName);
});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter();

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

app.Run();

void MigrationDatabase(string connection)
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
		Log.Error("Database migrate failed!", ex);
		throw;
	}
}