using HRMApiApp.BLL;
using HRMApiApp.BLL.Interfaces;
using HRMApiApp.DAL;
using HRMApiApp.DAL.Interfaces;
using HRMApiApp.Model;
using HRMApiApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<HanaHrmContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("con"))
    );


builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<ICommonRepository, CommonRepository>();
builder.Services.AddScoped<ICommonService, CommonService>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
