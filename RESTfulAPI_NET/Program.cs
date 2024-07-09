using Microsoft.EntityFrameworkCore;
using RESTfulAPI_NET.Business;
using RESTfulAPI_NET.Business.Implementations;
using RESTfulAPI_NET.Model.Context;
using RESTfulAPI_NET.Repository;
using RESTfulAPI_NET.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration["MySQLConnection:MySQLConnectionStribng"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    connection,
    new MySqlServerVersion(new Version(8,0,2))
));

// Versioning API
builder.Services.AddApiVersioning();

// Add Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
