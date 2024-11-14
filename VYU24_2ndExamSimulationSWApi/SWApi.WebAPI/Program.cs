using SWApi.Infrastructure.Contracts;
using SWApi.Infrastructure.Impl;
using SWApi.Infrastructure.Impl.ExternalConnections;
using SWApi.Services.Contracts;
using SWApi.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IPlanetsService, PlanetsService>();
builder.Services.AddScoped<IResidentsService, ResidentsService>();
builder.Services.AddScoped<ISWApiPlanetsRepository, SWApiPlanetsRepository>();
builder.Services.AddScoped<ISWDBPlanetsRepository, SWDBPlanetsRepository>();
builder.Services.AddScoped<ISWApiPeopleRepository, SWApiPeopleRepository>();
builder.Services.AddScoped<SWApiContext>();
builder.Services.AddScoped<SWDBContext>();

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
