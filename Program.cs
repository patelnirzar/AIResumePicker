using AIResumePicker.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add authorization services
builder.Services.AddAuthorization();

// Add controller services
builder.Services.AddControllers();

//Auto model mapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

