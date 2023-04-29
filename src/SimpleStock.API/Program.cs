using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SimpleStock.API.Middlewares;
using SimpleStock.Application.Interfaces;
using SimpleStock.Application.Profiles;
using SimpleStock.Application.Services;
using SimpleStock.Application.Validators.Product;
using SimpleStock.Data.Interfaces;
using SimpleStock.Data.Repositories;
using SimpleStock.Domain.Models;
using SimpleStock.Infrastructure.DataContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SimpleStockDataContext>(conf =>
{
    var connectionString = builder.Configuration.GetConnectionString("SimpleStock");
    conf.UseSqlite(connectionString);
});


builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateValidator>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddAutoMapper(typeof(ProductProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
