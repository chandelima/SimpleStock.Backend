using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SimpleStock.API.Middlewares;
using SimpleStock.Application.Interfaces;
using SimpleStock.Application.Profiles;
using SimpleStock.Application.Services;
using SimpleStock.Application.Validators;
using SimpleStock.Data.Interfaces;
using SimpleStock.Data.Repositories;
using SimpleStock.Domain.Models;
using SimpleStock.Infrastructure.DataContexts;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SimpleStockDataContext>(conf =>
{
    var connectionString = builder.Configuration.GetConnectionString("SimpleStock");
    conf.UseSqlite(connectionString).EnableSensitiveDataLogging();
});

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions
        .ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddressCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CustomerCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CustomerUpdateValidator>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

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
