using UniversitiesManagement.Infrastructure.Contracts;
using UniversitiesManagement.Infrastructure.Impl;
using UniversitiesManagement.Infrastructure.Impl.ExternalConnections;
using UniversitiesManagement.Services.Contracts;
using UniversitiesManagement.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUniversitiesService, UniversitiesService>();
builder.Services.AddScoped<IUniversitiesWebApiRepository, UniversitiesWebApiRepository>();
builder.Services.AddScoped<IUniversitiesDbRepository, UniversitiesDbRepository>();
builder.Services.AddScoped<UniversitiesManagementContext>();

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
