using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SmartBite.CustomerProfile.Application.Interfaces;
using SmartBite.CustomerProfile.Application.Mappers;
using SmartBite.CustomerProfile.Application.Services;
using SmartBite.CustomerProfile.Domain.Interfaces;
using SmartBite.CustomerProfile.Infrastructure.Persistance;
using SmartBite.CustomerProfile.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CustomerProfileDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerProfileDbConnection")));

builder.Services.AddAutoMapper(cfg => { }, typeof(CustomerProfile).Assembly);

builder.Services.AddScoped<ICustomerInfoRepository, CustomerInfoRepository>();

builder.Services.AddScoped<ICustomerInfoService, CustomerInfoService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
