using CountriesManagement.Infrastructure.Contracts;
using CountriesManagement.Infrastructure.Impl;
using CountriesManagement.Library.Contracts;
using CountriesManagement.Library.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICountryPopulationService, CountryPopulationService>();
builder.Services.AddScoped<ICountryPopulationRepository, CountryPopulationRepository>();

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
